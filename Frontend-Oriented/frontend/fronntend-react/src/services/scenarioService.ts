import { Scenario, CreateScenarioPayload } from '../types';

/**
 * Service for managing Scenarios
 */
class ScenarioService {
  private readonly baseUrl = '/api/scenarios';

  /**
   * Get all scenarios
   * TODO(candidate): Implement real API call
   */
  async listScenarios(): Promise<Scenario[]> {
    throw new Error('NOT_IMPLEMENTED: listScenarios');
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
    throw new Error('NOT_IMPLEMENTED: createScenario');
  }
}

export const scenarioService = new ScenarioService();
