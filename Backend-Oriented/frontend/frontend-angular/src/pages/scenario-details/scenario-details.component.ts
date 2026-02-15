import { Component, OnInit } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { RouterModule, Router, ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ScenarioService } from '../../services/scenario.service';
import { EntityService, EntityFilterParams } from '../../services/entity.service';
import { Scenario, Entity, EntityType, TaskForce } from '../../types';
import { LoadingIndicatorComponent } from '../../components/ui/loading-indicator/loading-indicator.component';
import { EmptyStateComponent } from '../../components/ui/empty-state/empty-state.component';
import { ErrorBannerComponent } from '../../components/ui/error-banner/error-banner.component';
import { EntityMapViewComponent } from '../../components/entity-map-view/entity-map-view.component';

const ENTITY_TYPES: EntityType[] = ['Soldier', 'Tank', 'Drone', 'Aircraft', 'Vehicle', 'Civilian'];
const TASK_FORCES: TaskForce[] = ['Friendly', 'Enemy'];

type EntitySortField = 'name' | 'type' | 'taskForce' | 'latitude' | 'longitude' | 'updatedAt';
type SortOrder = 'asc' | 'desc';
type ViewMode = 'table' | 'map';

/**
 * Scenario Details Component
 * Displays scenario info and its entities with filtering, sorting, and map view.
 * Filtering and sorting are performed server-side via query parameters.
 */
@Component({
  selector: 'app-scenario-details',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    DatePipe,
    LoadingIndicatorComponent,
    EmptyStateComponent,
    ErrorBannerComponent,
    EntityMapViewComponent
  ],
  templateUrl: './scenario-details.component.html',
  styleUrl: './scenario-details.component.css'
})
export class ScenarioDetailsComponent implements OnInit {
  scenario: Scenario | null = null;
  entities: Entity[] = [];
  loading = true;
  entitiesLoading = false;
  error: string | null = null;
  entitiesError: string | null = null;

  // Filter state
  entityTypes = ENTITY_TYPES;
  taskForces = TASK_FORCES;
  typeFilter = '';
  taskForceFilter = '';

  // Sort state
  sortBy: EntitySortField | '' = '';
  sortOrder: SortOrder = 'asc';

  // View mode
  viewMode: ViewMode = 'table';

  private scenarioId: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private scenarioService: ScenarioService,
    private entityService: EntityService
  ) {}

  ngOnInit() {
    this.scenarioId = this.route.snapshot.paramMap.get('id');
    if (!this.scenarioId) {
      this.error = 'Invalid scenario ID';
      this.loading = false;
      return;
    }

    this.loadScenario(this.scenarioId);
  }

  loadScenario(id: string) {
    this.loading = true;
    this.error = null;

    this.scenarioService.getScenario(id).subscribe({
      next: (scenario) => {
        this.scenario = scenario;
        this.loading = false;
        this.loadEntities(id);
      },
      error: (err) => {
        if (err.message === 'Scenario not found' || err.status === 404) {
          this.error = 'Scenario not found';
        } else {
          this.error = 'Failed to load scenario';
        }
        this.loading = false;
      }
    });
  }

  loadEntities(scenarioId?: string) {
    const id = scenarioId || this.scenarioId;
    if (!id) return;

    this.entitiesLoading = true;
    this.entitiesError = null;

    const filters: EntityFilterParams = {};
    if (this.typeFilter) filters.type = this.typeFilter;
    if (this.taskForceFilter) filters.taskForce = this.taskForceFilter;
    if (this.sortBy) {
      filters.sortBy = this.sortBy;
      filters.sortOrder = this.sortOrder;
    }

    this.entityService.listEntitiesByScenario(id, filters).subscribe({
      next: (entities) => {
        this.entities = entities;
        this.entitiesLoading = false;
      },
      error: () => {
        this.entitiesError = 'Failed to load entities';
        this.entitiesLoading = false;
      }
    });
  }

  onFilterChange() {
    this.loadEntities();
  }

  handleSort(field: EntitySortField) {
    if (this.sortBy === field) {
      this.sortOrder = this.sortOrder === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortBy = field;
      this.sortOrder = 'asc';
    }
    this.loadEntities();
  }

  getSortIndicator(field: EntitySortField): string {
    if (this.sortBy !== field) return ' ↕';
    return this.sortOrder === 'asc' ? ' ↑' : ' ↓';
  }

  setViewMode(mode: ViewMode) {
    this.viewMode = mode;
  }
}
