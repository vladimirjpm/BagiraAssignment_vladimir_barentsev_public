import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { scenarioService } from '../services/scenarioService';
import { LoadingIndicator } from '../components/ui/LoadingIndicator';
import { EmptyState } from '../components/ui/EmptyState';
import { ErrorBanner } from '../components/ui/ErrorBanner';
import './ScenarioListPage.css';

/**
 * Scenario List Page
 * TODO(candidate): Implement data fetching and binding to real data
 * TODO(candidate): Implement loading/empty/error states logic
 */
export const ScenarioListPage: React.FC = () => {
  const navigate = useNavigate();
  const [scenarios, setScenarios] = useState<any[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fetchScenarios = async () => {
    setLoading(true);
    setError(null);
    try {
      // const data = await scenarioService.listScenarios();
      // setScenarios(data);
      setScenarios([]); // Placeholder
    } catch (err) {
      setError('Failed to load scenarios');
    } finally {
      setLoading(false);
    }
  };


  // useEffect(() => { fetchScenarios(); }, []);

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
          message="No scenarios yet. Create your first scenario to get started."
          actionLabel="Create Scenario"
          onAction={() => navigate('/scenarios/new')}
        />
      ) : (
        <div className="scenarios-table-container">
          <table className="scenarios-table">
            <thead>
              <tr>
                <th>Name</th>
                <th>Description</th>
                <th>Entity Count</th>
                <th>Last Updated</th>
              </tr>
            </thead>
            <tbody>
              {scenarios.map((scenario) => (
                <tr key={scenario.id}>
                  <td>{scenario.name}</td>
                  <td className="description-cell">
                    {scenario.description || '-'}
                  </td>
                  <td>{scenario.entityCount || 0}</td>
                  <td>
                    {scenario.updatedAt
                      ? new Date(scenario.updatedAt).toLocaleDateString()
                      : '-'}
                  </td>
                  <td>
                    <button
                      className="action-button view"
                      onClick={() => navigate(`/scenarios/${scenario.id}`)}
                    >
                      View
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
