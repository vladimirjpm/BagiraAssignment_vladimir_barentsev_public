import React, { useEffect } from 'react';
import './SuccessNotification.css';

interface SuccessNotificationProps {
  message: string;
  onClose?: () => void;
  autoClose?: boolean;
  duration?: number;
}

/**
 * Success notification component
 */
export const SuccessNotification: React.FC<SuccessNotificationProps> = ({
  message,
  onClose,
  autoClose = true,
  duration = 3000,
}) => {
  useEffect(() => {
    if (autoClose && onClose) {
      const timer = setTimeout(() => {
        onClose();
      }, duration);
      return () => clearTimeout(timer);
    }
  }, [autoClose, duration, onClose]);

  return (
    <div className="success-notification" data-testid="success-notification">
      <span className="success-icon">✓</span>
      <span className="success-message">{message}</span>
      {onClose && (
        <button className="success-close-button" onClick={onClose}>
          ×
        </button>
      )}
    </div>
  );
};
