import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { entityService } from '../services/entityService';
import { EntityType, TaskForce } from '../types';
import { ErrorBanner } from '../components/ui/ErrorBanner';
import { LoadingIndicator } from '../components/ui/LoadingIndicator';
import './CreateEntityPage.css';

const ENTITY_TYPES: EntityType[] = ['Soldier', 'Tank', 'Drone', 'Aircraft', 'Vehicle', 'Civilian'];
const TASK_FORCES: TaskForce[] = ['Friendly', 'Enemy'];

/**
 * Create / Edit Entity Page.
 * Edit mode is entered via /scenarios/:scenarioId/entities/:entityId/edit.
 */
export const CreateEntityPage: React.FC = () => {
  const navigate = useNavigate();
  const { scenarioId, entityId } = useParams<{ scenarioId: string; entityId?: string }>();
  const isEditMode = Boolean(entityId);
  const [type, setType] = useState<EntityType>('Soldier');
  const [taskForce, setTaskForce] = useState<TaskForce>('Friendly');
  const [name, setName] = useState('');
  const [latitude, setLatitude] = useState('');
  const [longitude, setLongitude] = useState('');
  const [errors, setErrors] = useState<Record<string, string>>({});
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState(isEditMode);

  useEffect(() => {
    if (!scenarioId) {
      setError('Invalid scenario context');
      setTimeout(() => navigate('/scenarios'), 2000);
    }
  }, [scenarioId, navigate]);

  useEffect(() => {
    if (!entityId) return;

    const loadEntity = async () => {
      setLoading(true);
      setError(null);
      try {
        const entity = await entityService.getEntity(entityId);
        setType(entity.type);
        setTaskForce(entity.taskForce);
        setName(entity.name);
        setLatitude(String(entity.latitude));
        setLongitude(String(entity.longitude));
      } catch (err) {
        setError(err instanceof Error ? err.message : 'Failed to load entity');
      } finally {
        setLoading(false);
      }
    };

    loadEntity();
  }, [entityId]);

  const validate = (): boolean => {
    const newErrors: Record<string, string> = {};
    
    if (!type) {
      newErrors.type = 'Type is required';
    }
    
    if (!taskForce) {
      newErrors.taskForce = 'TaskForce is required';
    }
    
    if (!name.trim()) {
      newErrors.name = 'Name is required';
    }
    
    const latNum = parseFloat(latitude);
    if (isNaN(latNum) || latNum < -90 || latNum > 90) {
      newErrors.latitude = 'Latitude must be a number between -90 and 90';
    }
    
    const lonNum = parseFloat(longitude);
    if (isNaN(lonNum) || lonNum < -180 || lonNum > 180) {
      newErrors.longitude = 'Longitude must be a number between -180 and 180';
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    if (!scenarioId || !validate()) {
      return;
    }

    setSubmitting(true);
    setError(null);

    try {
      const payload = {
        type,
        taskForce,
        name,
        latitude: parseFloat(latitude),
        longitude: parseFloat(longitude),
      };
      if (isEditMode) {
        await entityService.updateEntity(entityId!, payload);
      } else {
        await entityService.createEntity(scenarioId, payload);
      }
      navigate(`/scenarios/${scenarioId}`);
    } catch (err: any) {
      if (err.message?.includes('validation')) {
        setError(err.message);
      } else {
        setError(`Failed to ${isEditMode ? 'update' : 'create'} entity`);
      }
    } finally {
      setSubmitting(false);
    }
  };

  if (!scenarioId) {
    return (
      <div className="create-entity-page">
        <ErrorBanner message="Invalid scenario context. Cannot create entity without a scenario." />
      </div>
    );
  }

  if (loading) {
    return <LoadingIndicator />;
  }

  return (
    <div className="create-entity-page">
      <div className="page-header">
        <h1>{isEditMode ? 'Edit Entity' : 'Add Entity'}</h1>
      </div>

      {error && <ErrorBanner message={error} />}

      <form onSubmit={handleSubmit} className="entity-form">
        <div className="form-group">
          <label htmlFor="type">
            Type <span className="required">*</span>
          </label>
          <select
            id="type"
            value={type}
            onChange={(e) => setType(e.target.value as EntityType)}
            className={errors.type ? 'error' : ''}
            disabled={submitting}
          >
            {ENTITY_TYPES.map((entityType) => (
              <option key={entityType} value={entityType}>
                {entityType}
              </option>
            ))}
          </select>
          {errors.type && <span className="field-error">{errors.type}</span>}
        </div>

        <div className="form-group">
          <label htmlFor="taskForce">
            TaskForce <span className="required">*</span>
          </label>
          <select
            id="taskForce"
            value={taskForce}
            onChange={(e) => setTaskForce(e.target.value as TaskForce)}
            className={errors.taskForce ? 'error' : ''}
            disabled={submitting}
          >
            {TASK_FORCES.map((tf) => (
              <option key={tf} value={tf}>
                {tf}
              </option>
            ))}
          </select>
          {errors.taskForce && <span className="field-error">{errors.taskForce}</span>}
        </div>

        <div className="form-group">
          <label htmlFor="name">
            Name / Callsign <span className="required">*</span>
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
          <label htmlFor="latitude">
            Latitude <span className="required">*</span>
          </label>
          <input
            id="latitude"
            type="number"
            step="any"
            value={latitude}
            onChange={(e) => setLatitude(e.target.value)}
            className={errors.latitude ? 'error' : ''}
            disabled={submitting}
            placeholder="-90 to 90"
          />
          {errors.latitude && <span className="field-error">{errors.latitude}</span>}
        </div>

        <div className="form-group">
          <label htmlFor="longitude">
            Longitude <span className="required">*</span>
          </label>
          <input
            id="longitude"
            type="number"
            step="any"
            value={longitude}
            onChange={(e) => setLongitude(e.target.value)}
            className={errors.longitude ? 'error' : ''}
            disabled={submitting}
            placeholder="-180 to 180"
          />
          {errors.longitude && <span className="field-error">{errors.longitude}</span>}
        </div>

        <div className="form-actions">
          <button
            type="button"
            className="secondary-button"
            onClick={() => navigate(`/scenarios/${scenarioId}`)}
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
