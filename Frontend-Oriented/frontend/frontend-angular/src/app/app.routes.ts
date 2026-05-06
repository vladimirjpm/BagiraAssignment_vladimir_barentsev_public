import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home';

export const routes: Routes = [
  // TODO: Add your routes here
  { path: '**', component: HomeComponent }
];
