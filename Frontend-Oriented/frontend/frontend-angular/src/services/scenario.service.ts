import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { Scenario, CreateScenarioPayload } from '../types';

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
   * Get all scenarios
   * TODO(candidate): Implement real API call
   */
  listScenarios(): Observable<Scenario[]> {
    return throwError(() => new Error('NOT_IMPLEMENTED: listScenarios'));
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
    return throwError(() => new Error('NOT_IMPLEMENTED: createScenario'));
  }
}
