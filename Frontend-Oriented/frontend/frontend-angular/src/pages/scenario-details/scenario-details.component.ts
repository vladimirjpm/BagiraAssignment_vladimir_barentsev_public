import { Component, OnInit } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { RouterModule, Router, ActivatedRoute } from '@angular/router';
import { ScenarioService } from '../../services/scenario.service';
import { EntityService } from '../../services/entity.service';
import { Scenario, Entity } from '../../types';
import { LoadingIndicatorComponent } from '../../components/ui/loading-indicator/loading-indicator.component';
import { EmptyStateComponent } from '../../components/ui/empty-state/empty-state.component';
import { ErrorBannerComponent } from '../../components/ui/error-banner/error-banner.component';

/**
 * Scenario Details Component
 */
@Component({
  selector: 'app-scenario-details',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    DatePipe,
    LoadingIndicatorComponent,
    EmptyStateComponent,
    ErrorBannerComponent
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

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private scenarioService: ScenarioService,
    private entityService: EntityService
  ) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (!id) {
      this.error = 'Invalid scenario ID';
      this.loading = false;
      return;
    }

    this.loadScenario(id);
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

  loadEntities(scenarioId: string) {
    this.entitiesLoading = true;
    this.entitiesError = null;

    this.entityService.listEntitiesByScenario(scenarioId).subscribe({
      next: (entities) => {
        this.entities = entities.filter(entity => entity.scenarioId === scenarioId);
        this.entitiesLoading = false;
      },
      error: (err) => {
        this.entitiesError = 'Failed to load entities';
        this.entitiesLoading = false;
      }
    });
  }

}
