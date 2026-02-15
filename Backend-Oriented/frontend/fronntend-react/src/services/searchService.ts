import { Scenario, Entity } from '../types';

export interface SearchResult {
  scenarios: Scenario[];
  entities: Entity[];
}

/**
 * Service for global search across scenarios and entities
 */
class SearchService {
  private readonly baseUrl = '/api/search';

  /**
   * Search across scenarios and entities
   */
  async search(query: string): Promise<SearchResult> {
    const params = new URLSearchParams({ q: query });
    const response = await fetch(`${this.baseUrl}?${params}`);
    if (!response.ok) {
      throw new Error(`Search failed: ${response.statusText}`);
    }
    return response.json();
  }
}

export const searchService = new SearchService();
