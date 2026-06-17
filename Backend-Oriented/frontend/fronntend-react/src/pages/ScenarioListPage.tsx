import React, { useState, useEffect, useCallback } from 'react';
import { useNavigate } from 'react-router-dom';
import { scenarioService, ScenarioFilterParams } from '../services/scenarioService';
import { Scenario } from '../types';
import { LoadingIndicator } from '../components/ui/LoadingIndicator';
import { EmptyState } from '../components/ui/EmptyState';
import { ErrorBanner } from '../components/ui/ErrorBanner';
import './ScenarioListPage.css';

type SortField = 'name' | 'description' | 'entityCount' | 'updatedAt';
type SortOrder = 'asc' | 'desc';

/**
 * Scenario List Page
 * Displays all scenarios with filtering by name/description and sortable columns.
 * Filtering and sorting are performed server-side via query parameters.
 */
export const ScenarioListPage: React.FC = () => {
  const navigate = useNavigate();
  const [scenarios, setScenarios] = useState<Scenario[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  // Filter state
  const [nameFilter, setNameFilter] = useState('');
  const [descriptionFilter, setDescriptionFilter] = useState('');

  // Sort state
  const [sortBy, setSortBy] = useState<SortField | ''>('');
  const [sortOrder, setSortOrder] = useState<SortOrder>('asc');

  const fetchScenarios = useCallback(async () => {
    setLoading(true);
    setError(null);
    try {
      const filters: ScenarioFilterParams = {};
      if (nameFilter.trim()) filters.name = nameFilter.trim();
      if (descriptionFilter.trim()) filters.description = descriptionFilter.trim();
      if (sortBy) {
        filters.sortBy = sortBy;
        filters.sortOrder = sortOrder;
      }
      const data = await scenarioService.listScenarios(filters);
      setScenarios(data);
    } catch (err) {
      setError('Failed to load scenarios');
    } finally {
      setLoading(false);
    }
  }, [nameFilter, descriptionFilter, sortBy, sortOrder]);

  useEffect(() => {
    fetchScenarios();
  }, [fetchScenarios]);

  const handleSort = (field: SortField) => {
    if (sortBy === field) {
      setSortOrder(prev => (prev === 'asc' ? 'desc' : 'asc'));
    } else {
      setSortBy(field);
      setSortOrder('asc');
    }
  };

  const getSortIndicator = (field: SortField) => {
    if (sortBy !== field) return ' ↕';
    return sortOrder === 'asc' ? ' ↑' : ' ↓';
  };

  const handleDelete = async (scenario: Scenario) => {
    if (!window.confirm(`Delete scenario "${scenario.name}" and all its entities?`)) {
      return;
    }
    try {
      await scenarioService.deleteScenario(scenario.id);
      await fetchScenarios();
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to delete scenario');
    }
  };

  return (
    <div className="scenario-list-page">
      <div className="page-header">
        <h1>Scenarios</h1>
        <button
          className="primary-button"
          onClick={() => navigate('/scenarios/new')}
        >
          Create Scenario
        </button>
      </div>

      <div className="filters-bar">
        <input
          type="text"
          placeholder="Filter by name..."
          value={nameFilter}
          onChange={(e) => setNameFilter(e.target.value)}
          className="filter-input"
        />
        <input
          type="text"
          placeholder="Filter by description..."
          value={descriptionFilter}
          onChange={(e) => setDescriptionFilter(e.target.value)}
          className="filter-input"
        />
      </div>

      {error && (
        <ErrorBanner
          message={error}
          onRetry={fetchScenarios}
        />
      )}

      {loading ? (
        <LoadingIndicator />
      ) : scenarios.length === 0 ? (
        <EmptyState
          message="No scenarios found. Create your first scenario to get started."
          actionLabel="Create Scenario"
          onAction={() => navigate('/scenarios/new')}
        />
      ) : (
        <div className="scenarios-table-container">
          <table className="scenarios-table">
            <thead>
              <tr>
                <th className="sortable-header" onClick={() => handleSort('name')}>
                  Name{getSortIndicator('name')}
                </th>
                <th className="sortable-header" onClick={() => handleSort('description')}>
                  Description{getSortIndicator('description')}
                </th>
                <th className="sortable-header" onClick={() => handleSort('entityCount')}>
                  Entity Count{getSortIndicator('entityCount')}
                </th>
                <th className="sortable-header" onClick={() => handleSort('updatedAt')}>
                  Last Updated{getSortIndicator('updatedAt')}
                </th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {scenarios.map((scenario) => (
                <tr key={scenario.id}>
                  <td>{scenario.name}</td>
                  <td className="description-cell">
                    {scenario.description || '-'}
                  </td>
                  <td>{scenario.entityCount ?? 0}</td>
                  <td>
                    {scenario.updatedAt
                      ? new Date(scenario.updatedAt).toLocaleDateString()
                      : '-'}
                  </td>
                  <td className="action-buttons">
                    <button
                      className="action-button view"
                      onClick={() => navigate(`/scenarios/${scenario.id}`)}
                    >
                      View
                    </button>
                    <button
                      className="action-button edit"
                      onClick={() => navigate(`/scenarios/${scenario.id}/edit`)}
                    >
                      Edit
                    </button>
                    <button
                      className="action-button delete"
                      onClick={() => handleDelete(scenario)}
                    >
                      Delete
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
};
