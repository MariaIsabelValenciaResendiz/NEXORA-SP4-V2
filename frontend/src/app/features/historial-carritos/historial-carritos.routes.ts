import { Routes } from '@angular/router';

import { historialCarritosGuard } from '../../core/guards/historial-carritos.guard';

export const HISTORIAL_CARRITOS_ROUTES: Routes = [
  {
    path: '',
    canActivate: [historialCarritosGuard],
    loadComponent: () => import('./view/historial-carritos').then((m) => m.HistorialCarritos)
  }
];