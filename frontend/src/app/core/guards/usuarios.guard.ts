import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

import { SessionService } from '../services/session.service';

export const usuariosGuard: CanActivateFn = () => {
  const usuario = inject(SessionService).obtener();
  const router = inject(Router);

  if (!usuario) {
    return router.createUrlTree(['/auth/login']);
  }

  return ['Administrador', 'Auditor'].includes(usuario.rol)
    ? true
    : router.createUrlTree(['/catalogo']);
};