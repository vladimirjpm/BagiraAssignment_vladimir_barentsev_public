import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ScenarioService, ScenarioFilterParams } from '../../services/scenario.service';
import { Scenario } from '../../types';
import { LoadingIndicatorComponent } from '../../components/ui/loading-indicator/loading-indicator.component';
import { EmptyStateComponent } from '../../components/ui/empty-state/empty-state.component';
import { ErrorBannerComponent } from '../../components/ui/error-banner/error-banner.component';

type SortField = 'name' | 'description' | 'entityCount' | 'updatedAt';
type SortOrder = 'asc' | 'desc';

/**
 * Scenario List Component
 * Displays all scenarios with filtering by name/description and sortable columns.
 * Filtering and sorting are performed server-side via query parameters.
 */
@Component({
  selector: 'app-scenario-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    LoadingIndicatorComponent,
    EmptyStateComponent,
    ErrorBannerComponent
  ],
  templateUrl: './scenario-list.component.html',
  styleUrl: './scenario-list.component.css'
})
export class ScenarioListComponent implements OnInit {
  scenarios: Scenario[] = [];
  loading = false;
  error: string | null = null;

  // Filter state
  nameFilter = '';
  descriptionFilter = '';

  // Sort state
  sortBy: SortField | '' = '';
  sortOrder: SortOrder = 'asc';

  constructor(private scenarioService: ScenarioService) {}

  ngOnInit() {
    this.loadScenarios();
  }

  loadScenarios() {
    this.loading = true;
    this.error = null;

    const filters: ScenarioFilterParams = {};
    if (this.nameFilter.trim()) filters.name = this.nameFilter.trim();
    if (this.descriptionFilter.trim()) filters.description = this.descriptionFilter.trim();
    if (this.sortBy) {
      filters.sortBy = this.sortBy;
      filters.sortOrder = this.sortOrder;
    }

    this.scenarioService.listScenarios(filters).subscribe({
      next: (data) => {
        this.scenarios = data;
        this.loading = false;
      },
      error: () => {
        this.error = 'Failed to load scenarios';
        this.loading = false;
      }
    });
  }

  onFilterChange() {
    this.loadScenarios();
  }

  handleSort(field: SortField) {
    if (this.sortBy === field) {
      this.sortOrder = this.sortOrder === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortBy = field;
      this.sortOrder = 'asc';
    }
    this.loadScenarios();
  }

  getSortIndicator(field: SortField): string {
    if (this.sortBy !== field) return ' ↕';
    return this.sortOrder === 'asc' ? ' ↑' : ' ↓';
  }
}
