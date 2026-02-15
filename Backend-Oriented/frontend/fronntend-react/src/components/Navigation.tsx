import React, { useState, useEffect, useRef } from 'react';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import { searchService, SearchResult } from '../services/searchService';
import './Navigation.css';

/**
 * Navigation component with global search bar
 */
export const Navigation: React.FC = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const [searchQuery, setSearchQuery] = useState('');
  const [searchResults, setSearchResults] = useState<SearchResult | null>(null);
  const [showResults, setShowResults] = useState(false);
  const [searching, setSearching] = useState(false);
  const searchRef = useRef<HTMLDivElement>(null);
  const debounceRef = useRef<ReturnType<typeof setTimeout>>();

  useEffect(() => {
    if (!searchQuery.trim()) {
      setSearchResults(null);
      setShowResults(false);
      return;
    }

    // Debounce search
    if (debounceRef.current) clearTimeout(debounceRef.current);
    debounceRef.current = setTimeout(async () => {
      setSearching(true);
      try {
        const results = await searchService.search(searchQuery.trim());
        setSearchResults(results);
        setShowResults(true);
      } catch {
        setSearchResults(null);
      } finally {
        setSearching(false);
      }
    }, 300);

    return () => {
      if (debounceRef.current) clearTimeout(debounceRef.current);
    };
  }, [searchQuery]);

  // Close dropdown when clicking outside
  useEffect(() => {
    const handleClickOutside = (e: MouseEvent) => {
      if (searchRef.current && !searchRef.current.contains(e.target as Node)) {
        setShowResults(false);
      }
    };
    document.addEventListener('mousedown', handleClickOutside);
    return () => document.removeEventListener('mousedown', handleClickOutside);
  }, []);

  // Close dropdown on navigation
  useEffect(() => {
    setShowResults(false);
    setSearchQuery('');
  }, [location.pathname]);

  const highlightMatch = (text: string, query: string) => {
    if (!query.trim()) return text;
    const regex = new RegExp(`(${query.replace(/[.*+?^${}()|[\]\\]/g, '\\$&')})`, 'gi');
    const parts = text.split(regex);
    return parts.map((part, i) =>
      regex.test(part) ? <mark key={i}>{part}</mark> : part
    );
  };

  const totalResults = searchResults
    ? searchResults.scenarios.length + searchResults.entities.length
    : 0;

  return (
    <nav className="navigation">
      <div className="nav-container">
        <Link to="/" className="nav-brand">
          Scenario Builder
        </Link>

        <div className="nav-search" ref={searchRef}>
          <input
            type="text"
            placeholder="Search scenarios and entities..."
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
            onFocus={() => searchResults && setShowResults(true)}
            className="search-input"
          />
          {searching && <span className="search-spinner" />}

          {showResults && searchResults && (
            <div className="search-dropdown">
              {totalResults === 0 ? (
                <div className="search-no-results">No results found</div>
              ) : (
                <>
                  {searchResults.scenarios.length > 0 && (
                    <div className="search-section">
                      <div className="search-section-title">Scenarios</div>
                      {searchResults.scenarios.map((s) => (
                        <div
                          key={s.id}
                          className="search-result-item"
                          onClick={() => navigate(`/scenarios/${s.id}`)}
                        >
                          <div className="search-result-name">
                            {highlightMatch(s.name, searchQuery)}
                          </div>
                          {s.description && (
                            <div className="search-result-detail">
                              {highlightMatch(s.description, searchQuery)}
                            </div>
                          )}
                        </div>
                      ))}
                    </div>
                  )}
                  {searchResults.entities.length > 0 && (
                    <div className="search-section">
                      <div className="search-section-title">Entities</div>
                      {searchResults.entities.map((e) => (
                        <div
                          key={e.id}
                          className="search-result-item"
                          onClick={() => navigate(`/scenarios/${e.scenarioId}`)}
                        >
                          <div className="search-result-name">
                            {highlightMatch(e.name, searchQuery)}
                          </div>
                          <div className="search-result-detail">
                            {e.type} &middot; {e.taskForce}
                          </div>
                        </div>
                      ))}
                    </div>
                  )}
                </>
              )}
            </div>
          )}
        </div>

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
