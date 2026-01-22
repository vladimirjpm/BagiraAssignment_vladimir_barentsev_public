import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { Entity, CreateEntityPayload } from '../types';

/**
 * Service for managing Entities
 */
@Injectable({
  providedIn: 'root'
})
export class EntityService {
  private readonly baseUrl = '/api';

  constructor(private http: HttpClient) {}

  /**
   * Get all entities for a specific scenario
   */
  listEntitiesByScenario(scenarioId: string): Observable<Entity[]> {
    return this.http.get<Entity[]>(`${this.baseUrl}/scenarios/${scenarioId}/entities`);
  }

  /**
   * Get a single entity by ID
   */
  getEntity(id: string): Observable<Entity> {
    return this.http.get<Entity>(`${this.baseUrl}/entities/${id}`);
  }

  /**
   * Create a new entity in a scenario
   */
  createEntity(scenarioId: string, payload: CreateEntityPayload): Observable<Entity> {
    return this.http.post<Entity>(`${this.baseUrl}/scenarios/${scenarioId}/entities`, payload);
  }
}
