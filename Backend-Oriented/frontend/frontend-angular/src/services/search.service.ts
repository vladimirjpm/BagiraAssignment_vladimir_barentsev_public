import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Scenario, Entity } from '../types';

export interface SearchResult {
  scenarios: Scenario[];
  entities: Entity[];
}

/**
 * Service for global search across scenarios and entities
 */
@Injectable({
  providedIn: 'root'
})
export class SearchService {
  private readonly baseUrl = '/api/search';

  constructor(private http: HttpClient) {}

  /**
   * Search across scenarios and entities
   */
  search(query: string): Observable<SearchResult> {
    const params = new HttpParams().set('q', query);
    return this.http.get<SearchResult>(this.baseUrl, { params });
  }
}
