/**
 * Type definitions for Scenario Builder application
 */

export type EntityType = 'Soldier' | 'Tank' | 'Drone' | 'Aircraft' | 'Vehicle' | 'Civilian';
export type TaskForce = 'Friendly' | 'Enemy';

export interface Scenario {
  id: string;
  name: string;
  description?: string;
  entityCount?: number;
  updatedAt?: string;
}

export interface Entity {
  id: string;
  scenarioId: string;
  type: EntityType;
  taskForce: TaskForce;
  name: string;
  latitude: number;
  longitude: number;
  updatedAt?: string;
}

export interface CreateScenarioPayload {
  name: string;
  description?: string;
}

export interface CreateEntityPayload {
  type: EntityType;
  taskForce: TaskForce;
  name: string;
  latitude: number;
  longitude: number;
}

export type UpdateScenarioPayload = CreateScenarioPayload;
export type UpdateEntityPayload = CreateEntityPayload;
