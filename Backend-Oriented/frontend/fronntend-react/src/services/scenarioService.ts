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
class ScenarioService {
  private readonly baseUrl = '/api/scenarios';

  /**
   * Get all scenarios with optional filtering and sorting
   * TODO(candidate): Implement real API call
   */
  async listScenarios(filters?: ScenarioFilterParams): Promise<Scenario[]> {
    throw new Error('NOT_IMPLEMENTED');
  }

  /**
   * Get a single scenario by ID
   */
  async getScenario(id: string): Promise<Scenario> {
    const response = await fetch(`${this.baseUrl}/${id}`);
    if (!response.ok) {
      if (response.status === 404) {
        throw new Error('Scenario not found');
      }
      throw new Error(`Failed to fetch scenario: ${response.statusText}`);
    }
    return response.json();
  }

  /**
   * Create a new scenario
   * TODO(candidate): Implement real API call
   */
  async createScenario(payload: CreateScenarioPayload): Promise<Scenario> {
    throw new Error('NOT_IMPLEMENTED');
  }
}

export const scenarioService = new ScenarioService();
