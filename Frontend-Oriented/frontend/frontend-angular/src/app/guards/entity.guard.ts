import { inject } from '@angular/core';
import { Router, ActivatedRouteSnapshot, CanActivateFn } from '@angular/router';

export const entityGuard: CanActivateFn = (route: ActivatedRouteSnapshot) => {
  const router = inject(Router);
  const scenarioId = route.paramMap.get('scenarioId');
  const entityId = route.paramMap.get('entityId');
  
  if (!scenarioId || scenarioId.trim() === '') {
    router.navigate(['/scenarios']);
    return false;
  }
  
  if (entityId && entityId.trim() === '') {
    router.navigate(['/scenarios', scenarioId]);
    return false;
  }
  
  return true;
};
