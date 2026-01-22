import React from 'react';
import { Link, useLocation } from 'react-router-dom';
import './Navigation.css';

/**
 * Navigation component
 * TODO(candidate): Add active state highlighting based on current route
 */
export const Navigation: React.FC = () => {
  const location = useLocation();

  return (
    <nav className="navigation">
      <div className="nav-container">
        <Link to="/" className="nav-brand">
          Scenario Builder
        </Link>
        <div className="nav-links">
          <Link
            to="/scenarios"
            className={`nav-link ${location.pathname.startsWith('/scenarios') ? 'active' : ''}`}
          >
            Scenarios
          </Link>
        </div>
      </div>
    </nav>
  );
};
