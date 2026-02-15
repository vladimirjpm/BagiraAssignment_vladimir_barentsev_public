import React from 'react';
import './EmptyState.css';

interface EmptyStateProps {
  message: string;
  actionLabel?: string;
  onAction?: () => void;
}

/**
 * Empty state component
 */
export const EmptyState: React.FC<EmptyStateProps> = ({ message, actionLabel, onAction }) => {
  return (
    <div className="empty-state" data-testid="empty-state">
      <p className="empty-message">{message}</p>
      {actionLabel && onAction && (
        <button className="empty-action-button" onClick={onAction}>
          {actionLabel}
        </button>
      )}
    </div>
  );
};
