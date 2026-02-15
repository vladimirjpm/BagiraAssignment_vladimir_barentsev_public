import React, { useState, useEffect, useCallback } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { scenarioService } from '../services/scenarioService';
import { entityService, EntityFilterParams } from '../services/entityService';
import { Scenario, Entity, EntityType, TaskForce } from '../types';
import { LoadingIndicator } from '../components/ui/LoadingIndicator';
import { EmptyState } from '../components/ui/EmptyState';
import { ErrorBanner } from '../components/ui/ErrorBanner';
import { EntityMapView } from '../components/EntityMapView';
import './ScenarioDetailsPage.css';

const ENTITY_TYPES: EntityType[] = ['Soldier', 'Tank', 'Drone', 'Aircraft', 'Vehicle', 'Civilian'];
const TASK_FORCES: TaskForce[] = ['Friendly', 'Enemy'];

type EntitySortField = 'name' | 'type' | 'taskForce' | 'latitude' | 'longitude' | 'updatedAt';
type SortOrder = 'asc' | 'desc';
type ViewMode = 'table' | 'map';

/**
 * Scenario Details Page
 * Displays scenario info and its entities with filtering, sorting, and map view.
 * Filtering and sorting are performed server-side via query parameters.
 */
export const ScenarioDetailsPage: React.FC = () => {
  const navigate = useNavigate();
  const { id } = useParams<{ id: string }>();
  const [scenario, setScenario] = useState<Scenario | null>(null);
  const [entities, setEntities] = useState<Entity[]>([]);
  const [loading, setLoading] = useState(true);
  const [entitiesLoading, setEntitiesLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [entitiesError, setEntitiesError] = useState<string | null>(null);

  // Filter state
  const [typeFilter, setTypeFilter] = useState<string>('');
  const [taskForceFilter, setTaskForceFilter] = useState<string>('');

  // Sort state
  const [sortBy, setSortBy] = useState<EntitySortField | ''>('');
  const [sortOrder, setSortOrder] = useState<SortOrder>('asc');

  // View mode
  const [viewMode, setViewMode] = useState<ViewMode>('table');

  useEffect(() => {
    const loadData = async () => {
      if (!id) {
        setError('Invalid scenario ID');
        setLoading(false);
        return;
      }

      setLoading(true);
      setError(null);

      try {
        const scenarioData = await scenarioService.getScenario(id);
        setScenario(scenarioData);
      } catch (err: any) {
        if (err.message === 'Scenario not found') {
          setError('Scenario not found');
        } else {
          setError('Failed to load scenario');
        }
      } finally {
        setLoading(false);
      }
    };

    loadData();
  }, [id]);

  const loadEntities = useCallback(async () => {
    if (!id || !scenario) return;

    setEntitiesLoading(true);
    setEntitiesError(null);

    try {
      const filters: EntityFilterParams = {};
      if (typeFilter) filters.type = typeFilter;
      if (taskForceFilter) filters.taskForce = taskForceFilter;
      if (sortBy) {
        filters.sortBy = sortBy;
        filters.sortOrder = sortOrder;
      }
      const entitiesData = await entityService.listEntitiesByScenario(id, filters);
      setEntities(entitiesData);
    } catch (err) {
      setEntitiesError('Failed to load entities');
    } finally {
      setEntitiesLoading(false);
    }
  }, [id, scenario, typeFilter, taskForceFilter, sortBy, sortOrder]);

  useEffect(() => {
    if (scenario) {
      loadEntities();
    }
  }, [scenario, loadEntities]);

  const handleSort = (field: EntitySortField) => {
    if (sortBy === field) {
      setSortOrder(prev => (prev === 'asc' ? 'desc' : 'asc'));
    } else {
      setSortBy(field);
      setSortOrder('asc');
    }
  };

  const getSortIndicator = (field: EntitySortField) => {
    if (sortBy !== field) return ' ↕';
    return sortOrder === 'asc' ? ' ↑' : ' ↓';
  };

  if (loading) {
    return <LoadingIndicator />;
  }

  if (error) {
    return (
      <div className="scenario-details-page">
        <ErrorBanner message={error} />
      </div>
    );
  }

  if (!scenario) {
    return (
      <div className="scenario-details-page">
        <ErrorBanner message="Scenario not found" />
      </div>
    );
  }

  return (
    <div className="scenario-details-page">
      <div className="scenario-header">
        <div className="scenario-info">
          <h1>{scenario.name}</h1>
          {scenario.description && (
            <p className="scenario-description">{scenario.description}</p>
          )}
        </div>
      </div>

      <div className="entities-section">
        <div className="section-header">
          <h2>Entities</h2>
          <div className="section-actions">
            <div className="view-toggle">
              <button
                className={`toggle-button ${viewMode === 'table' ? 'active' : ''}`}
                onClick={() => setViewMode('table')}
              >
                Table
              </button>
              <button
                className={`toggle-button ${viewMode === 'map' ? 'active' : ''}`}
                onClick={() => setViewMode('map')}
              >
                Map
              </button>
            </div>
            <button
              className="primary-button"
              onClick={() => navigate(`/scenarios/${id}/entities/new`)}
            >
              Add Entity
            </button>
          </div>
        </div>

        <div className="entity-filters">
          <select
            value={typeFilter}
            onChange={(e) => setTypeFilter(e.target.value)}
            className="filter-select"
          >
            <option value="">All Types</option>
            {ENTITY_TYPES.map((t) => (
              <option key={t} value={t}>{t}</option>
            ))}
          </select>
          <select
            value={taskForceFilter}
            onChange={(e) => setTaskForceFilter(e.target.value)}
            className="filter-select"
          >
            <option value="">All TaskForces</option>
            {TASK_FORCES.map((tf) => (
              <option key={tf} value={tf}>{tf}</option>
            ))}
          </select>
        </div>

        {entitiesError && (
          <ErrorBanner
            message={entitiesError}
            onRetry={loadEntities}
          />
        )}

        {entitiesLoading ? (
          <LoadingIndicator />
        ) : entities.length === 0 ? (
          <EmptyState
            message="No entities found. Add your first entity to get started."
            actionLabel="Add Entity"
            onAction={() => navigate(`/scenarios/${id}/entities/new`)}
          />
        ) : viewMode === 'map' ? (
          <EntityMapView entities={entities} />
        ) : (
          <div className="entities-table-container">
            <table className="entities-table">
              <thead>
                <tr>
                  <th className="sortable-header" onClick={() => handleSort('type')}>
                    Type{getSortIndicator('type')}
                  </th>
                  <th className="sortable-header" onClick={() => handleSort('taskForce')}>
                    TaskForce{getSortIndicator('taskForce')}
                  </th>
                  <th className="sortable-header" onClick={() => handleSort('name')}>
                    Name{getSortIndicator('name')}
                  </th>
                  <th className="sortable-header" onClick={() => handleSort('latitude')}>
                    Latitude{getSortIndicator('latitude')}
                  </th>
                  <th className="sortable-header" onClick={() => handleSort('longitude')}>
                    Longitude{getSortIndicator('longitude')}
                  </th>
                  <th className="sortable-header" onClick={() => handleSort('updatedAt')}>
                    Updated At{getSortIndicator('updatedAt')}
                  </th>
                </tr>
              </thead>
              <tbody>
                {entities.map((entity) => (
                  <tr key={entity.id}>
                    <td>{entity.type}</td>
                    <td>{entity.taskForce}</td>
                    <td>{entity.name}</td>
                    <td>{entity.latitude}</td>
                    <td>{entity.longitude}</td>
                    <td>
                      {entity.updatedAt
                        ? new Date(entity.updatedAt).toLocaleDateString()
                        : '-'}
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        )}
      </div>
    </div>
  );
};
