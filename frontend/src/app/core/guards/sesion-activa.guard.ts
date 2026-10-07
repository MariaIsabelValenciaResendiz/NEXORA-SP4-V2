import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

import { SessionService } from '../services/session.service';

export const sesionActivaGuard: CanActivateFn = () => {
  const sesion = inject(SessionService);
  const router = inject(Router);

  return sesion.obtener() ? true : router.createUrlTree(['/auth/login']);
};