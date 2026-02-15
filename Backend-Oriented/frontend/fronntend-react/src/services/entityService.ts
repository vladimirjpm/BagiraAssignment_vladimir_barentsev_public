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
class EntityService {
  private readonly baseUrl = '/api';

  /**
   * Get all entities for a specific scenario with optional filtering and sorting
   */
  async listEntitiesByScenario(scenarioId: string, filters?: EntityFilterParams): Promise<Entity[]> {
    const params = new URLSearchParams();
    if (filters?.type) params.append('type', filters.type);
    if (filters?.taskForce) params.append('taskForce', filters.taskForce);
    if (filters?.sortBy) params.append('sortBy', filters.sortBy);
    if (filters?.sortOrder) params.append('sortOrder', filters.sortOrder);

    const query = params.toString();
    const url = query
      ? `${this.baseUrl}/entities/scenarios/${scenarioId}/entities?${query}`
      : `${this.baseUrl}/entities/scenarios/${scenarioId}/entities`;

    const response = await fetch(url);
    if (!response.ok) {
      throw new Error(`Failed to fetch entities: ${response.statusText}`);
    }
    return response.json();
  }

  /**
   * Get a single entity by ID
   */
  async getEntity(id: string): Promise<Entity> {
    const response = await fetch(`${this.baseUrl}/entities/${id}`);
    if (!response.ok) {
      if (response.status === 404) {
        throw new Error('Entity not found');
      }
      throw new Error(`Failed to fetch entity: ${response.statusText}`);
    }
    return response.json();
  }

  /**
   * Create a new entity in a scenario
   */
  async createEntity(scenarioId: string, payload: CreateEntityPayload): Promise<Entity> {
    const response = await fetch(`${this.baseUrl}/entities/scenarios/${scenarioId}/entities`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(payload),
    });
    if (!response.ok) {
      const error = await response.json().catch(() => null);
      throw new Error(error?.message || `Failed to create entity: ${response.statusText}`);
    }
    return response.json();
  }
}

export const entityService = new EntityService();
