import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { scenarioService } from '../services/scenarioService';
import { entityService } from '../services/entityService';
import { Scenario, Entity } from '../types';
import { LoadingIndicator } from '../components/ui/LoadingIndicator';
import { EmptyState } from '../components/ui/EmptyState';
import { ErrorBanner } from '../components/ui/ErrorBanner';
import './ScenarioDetailsPage.css';

/**
 * Scenario Details Page
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

  useEffect(() => {
    const loadEntities = async () => {
      if (!id || !scenario) return;

      setEntitiesLoading(true);
      setEntitiesError(null);

      try {
        const entitiesData = await entityService.listEntitiesByScenario(id);
        setEntities(entitiesData.filter(entity => entity.scenarioId === id));
      } catch (err) {
        setEntitiesError('Failed to load entities');
      } finally {
        setEntitiesLoading(false);
      }
    };

    if (scenario) {
      loadEntities();
    }
  }, [id, scenario]);


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
          <button
            className="primary-button"
            onClick={() => navigate(`/scenarios/${id}/entities/new`)}
          >
            Add Entity
          </button>
        </div>

        {entitiesError && (
          <ErrorBanner
            message={entitiesError}
            onRetry={() => {
              if (id) {
                const loadEntities = async () => {
                  setEntitiesLoading(true);
                  setEntitiesError(null);
                  try {
                    const entitiesData = await entityService.listEntitiesByScenario(id);
                    setEntities(entitiesData.filter(entity => entity.scenarioId === id));
                  } catch (err) {
                    setEntitiesError('Failed to load entities');
                  } finally {
                    setEntitiesLoading(false);
                  }
                };
                loadEntities();
              }
            }}
          />
        )}

        {entitiesLoading ? (
          <LoadingIndicator />
        ) : entities.length === 0 ? (
          <EmptyState
            message="No entities in this scenario yet. Add your first entity to get started."
            actionLabel="Add Entity"
            onAction={() => navigate(`/scenarios/${id}/entities/new`)}
          />
        ) : (
          <div className="entities-table-container">
            <table className="entities-table">
              <thead>
                <tr>
                  <th>Type</th>
                  <th>TaskForce</th>
                  <th>Name</th>
                  <th>Latitude</th>
                  <th>Longitude</th>
                  <th>Updated At</th>
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
