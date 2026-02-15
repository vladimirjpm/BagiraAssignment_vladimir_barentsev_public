import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Scenario, CreateScenarioPayload } from '../types';

export interface ScenarioFilterParams {
  name?: string;
  description?: string;
  sortBy?: string;
  sortOrder?: 'asc' | 'desc';
}

/**
 * Service for managing Scenarios
 */
@Injectable({
  providedIn: 'root'
})
export class ScenarioService {
  private readonly baseUrl = '/api/scenarios';

  constructor(private http: HttpClient) {}

  /**
   * Get all scenarios with optional filtering and sorting
   * TODO(candidate): Implement real API call
   */
  listScenarios(filters?: ScenarioFilterParams): Observable<Scenario[]> {
    let params = new HttpParams();
    if (filters?.name) params = params.set('name', filters.name);
    if (filters?.description) params = params.set('description', filters.description);
    if (filters?.sortBy) params = params.set('sortBy', filters.sortBy);
    if (filters?.sortOrder) params = params.set('sortOrder', filters.sortOrder);

    return this.http.get<Scenario[]>(this.baseUrl, { params });
  }

  /**
   * Get a single scenario by ID
   */
  getScenario(id: string): Observable<Scenario> {
    return this.http.get<Scenario>(`${this.baseUrl}/${id}`);
  }

  /**
   * Create a new scenario
   * TODO(candidate): Implement real API call
   */
  createScenario(payload: CreateScenarioPayload): Observable<Scenario> {
    return this.http.post<Scenario>(this.baseUrl, payload);
  }
}
