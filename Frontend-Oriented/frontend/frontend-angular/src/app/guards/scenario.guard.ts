import { inject } from '@angular/core';
import { Router, ActivatedRouteSnapshot, CanActivateFn } from '@angular/router';

export const scenarioGuard: CanActivateFn = (route: ActivatedRouteSnapshot) => {
  const router = inject(Router);
  const id = route.paramMap.get('id');
  
  if (!id || id.trim() === '') {
    router.navigate(['/scenarios']);
    return false;
  }
  
  return true;
};
