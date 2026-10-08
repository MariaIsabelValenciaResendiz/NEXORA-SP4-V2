import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

import { SessionService } from '../services/session.service';

export const administradorGuard: CanActivateFn = () => {
  const sesion = inject(SessionService);
  const router = inject(Router);

  const usuario = sesion.obtener();

  return usuario?.rol === 'Administrador' ? true : router.createUrlTree(['/catalogo']);
};
