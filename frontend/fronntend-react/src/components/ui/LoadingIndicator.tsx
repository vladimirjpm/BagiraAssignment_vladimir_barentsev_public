import React from 'react';
import './LoadingIndicator.css';

/**
 * Loading indicator component
 */
export const LoadingIndicator: React.FC = () => {
  return (
    <div className="loading-indicator" data-testid="loading-indicator">
      <div className="spinner"></div>
      <p>Loading...</p>
    </div>
  );
};
