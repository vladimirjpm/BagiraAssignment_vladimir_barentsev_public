import React from 'react';
import { useParams, Navigate } from 'react-router-dom';

/**
 * Guard component to validate scenario and entity ID format
 */
export const EntityGuard: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const { scenarioId, entityId } = useParams<{ scenarioId?: string; entityId?: string }>();
  
  if (!scenarioId || scenarioId.trim() === '') {
    return <Navigate to="/scenarios" replace />;
  }
  
  if (entityId && entityId.trim() === '') {
    return <Navigate to={`/scenarios/${scenarioId}`} replace />;
  }
  
  return <>{children}</>;
};
