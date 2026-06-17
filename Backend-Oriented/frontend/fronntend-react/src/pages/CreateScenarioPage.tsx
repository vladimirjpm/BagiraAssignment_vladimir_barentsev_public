import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { scenarioService } from '../services/scenarioService';
import { ErrorBanner } from '../components/ui/ErrorBanner';
import { LoadingIndicator } from '../components/ui/LoadingIndicator';
import './CreateScenarioPage.css';

/**
 * Create / Edit Scenario Page.
 * Edit mode is entered via /scenarios/:id/edit — the same form is reused for both flows.
 */
export const CreateScenarioPage: React.FC = () => {
  const navigate = useNavigate();
  const { id } = useParams<{ id: string }>();
  const isEditMode = Boolean(id);

  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [errors, setErrors] = useState<Record<string, string>>({});
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState(isEditMode);

  useEffect(() => {
    if (!id) return;

    const loadScenario = async () => {
      setLoading(true);
      setError(null);
      try {
        const scenario = await scenarioService.getScenario(id);
        setName(scenario.name);
        setDescription(scenario.description ?? '');
      } catch (err) {
        setError(err instanceof Error ? err.message : 'Failed to load scenario');
      } finally {
        setLoading(false);
      }
    };

    loadScenario();
  }, [id]);

  const validate = (): boolean => {
    const newErrors: Record<string, string> = {};

    if (!name.trim()) {
      newErrors.name = 'Name is required';
    } else if (name.trim().length > 100) {
      newErrors.name = 'Name must be at most 100 characters';
    }
    if (description.length > 500) {
      newErrors.description = 'Description must be at most 500 characters';
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!validate()) {
      return;
    }

    setSubmitting(true);
    setError(null);

    try {
      const payload = {
        name: name.trim(),
        description: description.trim() || undefined,
      };
      const scenario = isEditMode
        ? await scenarioService.updateScenario(id!, payload)
        : await scenarioService.createScenario(payload);
      navigate(`/scenarios/${scenario.id}`);
    } catch (err) {
      setError(err instanceof Error ? err.message : `Failed to ${isEditMode ? 'update' : 'create'} scenario`);
    } finally {
      setSubmitting(false);
    }
  };

  if (loading) {
    return <LoadingIndicator />;
  }

  return (
    <div className="create-scenario-page">
      <div className="page-header">
        <h1>{isEditMode ? 'Edit Scenario' : 'Create Scenario'}</h1>
      </div>

      {error && <ErrorBanner message={error} />}

      <form onSubmit={handleSubmit} className="scenario-form">
        <div className="form-group">
          <label htmlFor="name">
            Name <span className="required">*</span>
          </label>
          <input
            id="name"
            type="text"
            value={name}
            onChange={(e) => setName(e.target.value)}
            className={errors.name ? 'error' : ''}
            disabled={submitting}
          />
          {errors.name && <span className="field-error">{errors.name}</span>}
        </div>

        <div className="form-group">
          <label htmlFor="description">Description</label>
          <textarea
            id="description"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            rows={4}
            disabled={submitting}
          />
        </div>

        <div className="form-actions">
          <button
            type="button"
            className="secondary-button"
            onClick={() => navigate(isEditMode ? `/scenarios/${id}` : '/scenarios')}
            disabled={submitting}
          >
            Cancel
          </button>
          <button
            type="submit"
            className="primary-button"
            disabled={submitting}
          >
            {submitting ? 'Saving...' : 'Save'}
          </button>
        </div>
      </form>
    </div>
  );
};
