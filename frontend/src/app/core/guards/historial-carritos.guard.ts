import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

import { SessionService } from '../services/session.service';

export const historialCarritosGuard: CanActivateFn = () => {
  const usuario = inject(SessionService).obtener();
  const router = inject(Router);

  if (!usuario) {
    return router.createUrlTree(['/auth/login']);
  }

  return usuario.rol === 'Administrador' || usuario.rol === 'Auditor'
    ? true
    : router.createUrlTree(['/catalogo']);
};