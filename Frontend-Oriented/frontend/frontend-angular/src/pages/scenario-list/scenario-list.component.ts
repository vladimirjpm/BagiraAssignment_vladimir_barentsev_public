import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ScenarioService } from '../../services/scenario.service';
import { Scenario } from '../../types';
import { LoadingIndicatorComponent } from '../../components/ui/loading-indicator/loading-indicator.component';
import { EmptyStateComponent } from '../../components/ui/empty-state/empty-state.component';
import { ErrorBannerComponent } from '../../components/ui/error-banner/error-banner.component';

/**
 * Scenario List Component
 * TODO(candidate): Implement data fetching and binding to real data
 * TODO(candidate): Implement loading/empty/error states logic
 */
@Component({
  selector: 'app-scenario-list',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
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

  constructor(private scenarioService: ScenarioService) {}

  ngOnInit() {
    // this.loadScenarios();
  }

  loadScenarios() {
    this.loading = true;
    this.error = null;
    
    // this.scenarioService.listScenarios().subscribe({
    //   next: (data) => {
    //     this.scenarios = data;
    //     this.loading = false;
    //   },
    //   error: (err) => {
    //     this.error = 'Failed to load scenarios';
    //     this.loading = false;
    //   }
    // });
  }


  onCreateScenarioClick() {
    // This will be handled by routerLink in template
  }
}
