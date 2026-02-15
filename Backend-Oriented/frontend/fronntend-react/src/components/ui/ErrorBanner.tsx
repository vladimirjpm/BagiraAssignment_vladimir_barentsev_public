import React from 'react';
import './ErrorBanner.css';

interface ErrorBannerProps {
  message: string;
  onRetry?: () => void;
}

/**
 * Error banner component
 */
export const ErrorBanner: React.FC<ErrorBannerProps> = ({ message, onRetry }) => {
  return (
    <div className="error-banner" data-testid="error-banner">
      <div className="error-content">
        <span className="error-icon">⚠️</span>
        <span className="error-message">{message}</span>
      </div>
      {onRetry && (
        <button className="error-retry-button" onClick={onRetry}>
          Retry
        </button>
      )}
    </div>
  );
};
