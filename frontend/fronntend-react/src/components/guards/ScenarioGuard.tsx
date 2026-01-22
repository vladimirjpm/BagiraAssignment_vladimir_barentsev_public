import React from 'react';
import { useParams, Navigate } from 'react-router-dom';

/**
 * Guard component to validate scenario ID format
 */
export const ScenarioGuard: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const { id } = useParams<{ id: string }>();
  
  if (!id || id.trim() === '') {
    return <Navigate to="/scenarios" replace />;
  }
  
  return <>{children}</>;
};
