import { Entity, CreateEntityPayload } from '../types';

/**
 * Service for managing Entities
 */
class EntityService {
  private readonly baseUrl = '/api';

  /**
   * Get all entities for a specific scenario
   */
  async listEntitiesByScenario(scenarioId: string): Promise<Entity[]> {
    const response = await fetch(`${this.baseUrl}/scenarios/${scenarioId}/entities`);
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
    const response = await fetch(`${this.baseUrl}/scenarios/${scenarioId}/entities`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(payload),
    });
    if (!response.ok) {
      throw new Error(`Failed to create entity: ${response.statusText}`);
    }
    return response.json();
  }
}

export const entityService = new EntityService();
