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
    const params = new URLSearchParams();
    if (filters?.name) params.append('name', filters.name);
    if (filters?.description) params.append('description', filters.description);
    if (filters?.sortBy) params.append('sortBy', filters.sortBy);
    if (filters?.sortOrder) params.append('sortOrder', filters.sortOrder);

    const query = params.toString();
    const url = query ? `${this.baseUrl}?${query}` : this.baseUrl;

    const response = await fetch(url);
    if (!response.ok) {
      throw new Error(`Failed to fetch scenarios: ${response.statusText}`);
    }
    return response.json();
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
    const response = await fetch(this.baseUrl, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(payload),
    });
    if (!response.ok) {
      const error = await response.json().catch(() => null);
      throw new Error(error?.message || `Failed to create scenario: ${response.statusText}`);
    }
    return response.json();
  }
}

export const scenarioService = new ScenarioService();
