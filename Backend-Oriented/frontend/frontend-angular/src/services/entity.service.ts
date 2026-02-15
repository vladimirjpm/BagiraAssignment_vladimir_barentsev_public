import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Entity, CreateEntityPayload } from '../types';

export interface EntityFilterParams {
  type?: string;
  taskForce?: string;
  sortBy?: string;
  sortOrder?: 'asc' | 'desc';
}

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
   * Get all entities for a specific scenario with optional filtering and sorting
   */
  listEntitiesByScenario(scenarioId: string, filters?: EntityFilterParams): Observable<Entity[]> {
    let params = new HttpParams();
    if (filters?.type) params = params.set('type', filters.type);
    if (filters?.taskForce) params = params.set('taskForce', filters.taskForce);
    if (filters?.sortBy) params = params.set('sortBy', filters.sortBy);
    if (filters?.sortOrder) params = params.set('sortOrder', filters.sortOrder);

    return this.http.get<Entity[]>(`${this.baseUrl}/entities/scenarios/${scenarioId}/entities`, { params });
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
    return this.http.post<Entity>(`${this.baseUrl}/entities/scenarios/${scenarioId}/entities`, payload);
  }
}
