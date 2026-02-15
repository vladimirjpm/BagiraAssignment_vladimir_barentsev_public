import React, { useEffect, useRef } from 'react';
import L from 'leaflet';
import 'leaflet/dist/leaflet.css';
import { Entity } from '../types';

interface EntityMapViewProps {
  entities: Entity[];
}

const TASK_FORCE_COLORS: Record<string, string> = {
  Friendly: '#2ecc71',
  Enemy: '#e74c3c',
};

/**
 * Map view component that displays entities on a Leaflet map.
 * Markers are colored by TaskForce (green=Friendly, red=Enemy).
 * Clicking a marker shows entity details in a popup.
 */
export const EntityMapView: React.FC<EntityMapViewProps> = ({ entities }) => {
  const mapRef = useRef<HTMLDivElement>(null);
  const mapInstanceRef = useRef<L.Map | null>(null);

  useEffect(() => {
    if (!mapRef.current) return;

    // Initialize map if not already created
    if (!mapInstanceRef.current) {
      mapInstanceRef.current = L.map(mapRef.current).setView([0, 0], 2);
      L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; OpenStreetMap contributors',
      }).addTo(mapInstanceRef.current);
    }

    const map = mapInstanceRef.current;

    // Clear existing markers
    map.eachLayer((layer) => {
      if (layer instanceof L.CircleMarker) {
        map.removeLayer(layer);
      }
    });

    // Add entity markers
    const markers: L.CircleMarker[] = [];
    entities.forEach((entity) => {
      const color = TASK_FORCE_COLORS[entity.taskForce] || '#3498db';
      const marker = L.circleMarker([entity.latitude, entity.longitude], {
        radius: 8,
        fillColor: color,
        color: '#fff',
        weight: 2,
        opacity: 1,
        fillOpacity: 0.8,
      }).addTo(map);

      marker.bindPopup(`
        <div class="entity-popup">
          <h4>${entity.name}</h4>
          <p><strong>Type:</strong> ${entity.type}</p>
          <p><strong>TaskForce:</strong> ${entity.taskForce}</p>
          <p><strong>Lat:</strong> ${entity.latitude}, <strong>Lon:</strong> ${entity.longitude}</p>
        </div>
      `);

      markers.push(marker);
    });

    // Fit map to markers if any exist
    if (markers.length > 0) {
      const group = L.featureGroup(markers);
      map.fitBounds(group.getBounds().pad(0.1));
    }

    return () => {
      // Cleanup on unmount
    };
  }, [entities]);

  useEffect(() => {
    return () => {
      if (mapInstanceRef.current) {
        mapInstanceRef.current.remove();
        mapInstanceRef.current = null;
      }
    };
  }, []);

  return <div ref={mapRef} className="map-container" />;
};
