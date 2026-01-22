import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { Navigation } from './components/Navigation';
import { ScenarioListPage } from './pages/ScenarioListPage';
import { CreateScenarioPage } from './pages/CreateScenarioPage';
import { ScenarioDetailsPage } from './pages/ScenarioDetailsPage';
import { CreateEntityPage } from './pages/CreateEntityPage';
import { ScenarioGuard } from './components/guards/ScenarioGuard';
import { EntityGuard } from './components/guards/EntityGuard';
import './App.css';

/**
 * Main App Component
 * Routing is set up for all screens
 */
function App() {
  return (
    <Router>
      <div className="app">
        <Navigation />
        <main className="app-content">
          <Routes>
            <Route path="/" element={<Navigate to="/scenarios" replace />} />
            <Route path="/scenarios" element={<ScenarioListPage />} />
            <Route path="/scenarios/new" element={<CreateScenarioPage />} />
            <Route
              path="/scenarios/:id"
              element={
                <ScenarioGuard>
                  <ScenarioDetailsPage />
                </ScenarioGuard>
              }
            />
            <Route
              path="/scenarios/:scenarioId/entities/new"
              element={
                <EntityGuard>
                  <CreateEntityPage />
                </EntityGuard>
              }
            />
          </Routes>
        </main>
      </div>
    </Router>
  );
}

export default App;
