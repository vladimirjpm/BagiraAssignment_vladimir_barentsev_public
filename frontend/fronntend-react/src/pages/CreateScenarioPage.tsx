import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { scenarioService } from '../services/scenarioService';
import { ErrorBanner } from '../components/ui/ErrorBanner';
import './CreateScenarioPage.css';

/**
 * Create Scenario Page
 * TODO(candidate): Implement real validation rules (beyond "required" marker text)
 * TODO(candidate): Implement submit behavior (calling service, disabling save during submit, navigation on success)
 * TODO(candidate): Display server validation messages
 */
export const CreateScenarioPage: React.FC = () => {
  const navigate = useNavigate();
  const [name, setName] = useState('');
  const [description, setDescription] = useState('');
  const [errors, setErrors] = useState<Record<string, string>>({});
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const validate = (): boolean => {
    const newErrors: Record<string, string> = {};
    
    if (!name.trim()) {
      newErrors.name = 'Name is required';
    }
    // Add minimum/maximum length constraints

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
      // const scenario = await scenarioService.createScenario({ name, description });
      // navigate(`/scenarios/${scenario.id}`);
      console.log('Create scenario:', { name, description });
      // Placeholder: navigate back to list
      navigate('/scenarios');
    } catch (err) {
      setError('Failed to create scenario');
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div className="create-scenario-page">
      <div className="page-header">
        <h1>Create Scenario</h1>
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
            onClick={() => navigate('/scenarios')}
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
