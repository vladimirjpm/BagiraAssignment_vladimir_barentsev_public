import { Component, Input, OnChanges, SimpleChanges, AfterViewInit, OnDestroy, ElementRef, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import * as L from 'leaflet';
import { Entity } from '../../types';

const TASK_FORCE_COLORS: Record<string, string> = {
  Friendly: '#2ecc71',
  Enemy: '#e74c3c',
};

/**
 * Map view component that displays entities on a Leaflet map.
 * Markers are colored by TaskForce (green=Friendly, red=Enemy).
 * Clicking a marker shows entity details in a popup.
 */
@Component({
  selector: 'app-entity-map-view',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './entity-map-view.component.html',
  styleUrl: './entity-map-view.component.css'
})
export class EntityMapViewComponent implements AfterViewInit, OnChanges, OnDestroy {
  @Input() entities: Entity[] = [];
  @ViewChild('mapContainer') mapContainer!: ElementRef;

  private map: L.Map | null = null;
  private markers: L.CircleMarker[] = [];

  ngAfterViewInit() {
    this.initMap();
    this.updateMarkers();
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['entities'] && this.map) {
      this.updateMarkers();
    }
  }

  ngOnDestroy() {
    if (this.map) {
      this.map.remove();
      this.map = null;
    }
  }

  private initMap() {
    if (!this.mapContainer?.nativeElement) return;

    this.map = L.map(this.mapContainer.nativeElement).setView([0, 0], 2);
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution: '&copy; OpenStreetMap contributors',
    }).addTo(this.map);
  }

  private updateMarkers() {
    if (!this.map) return;

    // Clear existing markers
    this.markers.forEach(m => this.map!.removeLayer(m));
    this.markers = [];

    // Add entity markers
    this.entities.forEach((entity) => {
      const color = TASK_FORCE_COLORS[entity.taskForce] || '#3498db';
      const marker = L.circleMarker([entity.latitude, entity.longitude], {
        radius: 8,
        fillColor: color,
        color: '#fff',
        weight: 2,
        opacity: 1,
        fillOpacity: 0.8,
      }).addTo(this.map!);

      marker.bindPopup(`
        <div class="entity-popup">
          <h4>${entity.name}</h4>
          <p><strong>Type:</strong> ${entity.type}</p>
          <p><strong>TaskForce:</strong> ${entity.taskForce}</p>
          <p><strong>Lat:</strong> ${entity.latitude}, <strong>Lon:</strong> ${entity.longitude}</p>
        </div>
      `);

      this.markers.push(marker);
    });

    // Fit map to markers if any exist
    if (this.markers.length > 0) {
      const group = L.featureGroup(this.markers);
      this.map.fitBounds(group.getBounds().pad(0.1));
    }
  }
}
