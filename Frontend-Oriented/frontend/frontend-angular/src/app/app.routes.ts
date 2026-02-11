import { Routes } from '@angular/router';
import { scenarioGuard } from './guards/scenario.guard';
import { entityGuard } from './guards/entity.guard';

/**
 * Application Routes
 */
export const routes: Routes = [
  {
    path: '',
    redirectTo: '/scenarios',
    pathMatch: 'full'
  },
  {
    path: 'scenarios',
    loadComponent: () => import('../pages/scenario-list/scenario-list.component').then(m => m.ScenarioListComponent)
  },
  {
    path: 'scenarios/new',
    loadComponent: () => import('../pages/create-scenario/create-scenario.component').then(m => m.CreateScenarioComponent)
  },
  {
    path: 'scenarios/:id',
    loadComponent: () => import('../pages/scenario-details/scenario-details.component').then(m => m.ScenarioDetailsComponent),
    canActivate: [scenarioGuard]
  },
  {
    path: 'scenarios/:scenarioId/entities/new',
    loadComponent: () => import('../pages/create-entity/create-entity.component').then(m => m.CreateEntityComponent),
    canActivate: [entityGuard]
  }
];
