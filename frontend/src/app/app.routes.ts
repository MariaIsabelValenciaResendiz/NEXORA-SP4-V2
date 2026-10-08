import { Routes } from '@angular/router';

import { sesionActivaGuard } from './core/guards/sesion-activa.guard';
import { AuthLayout } from './layouts/auth-layout/auth-layout';
import { MainShell } from './layouts/main-shell/main-shell';

export const routes: Routes = [
  {
    path: 'auth',
    component: AuthLayout,
    children: [
      {
        path: 'login',
        loadComponent: () => import('./features/login/view/login').then(m => m.Login)
      },
      { path: '', pathMatch: 'full', redirectTo: 'login' }
    ]
  },
  {
    path: '',
    component: MainShell,
    canActivate: [sesionActivaGuard],
    children: [
      { path: '', pathMatch: 'full', redirectTo: 'catalogo' },
      {
        path: 'catalogo',
        loadComponent: () =>
          import('./features/catalogo-productos/view/catalogo-productos').then(
            (m) => m.CatalogoProductos,
          ),
      },
      {
        path: 'carrito',
        loadComponent: () => import('./features/carrito/view/carrito').then((m) => m.Carrito),
      },
      {
        path: 'producto/:id',
        loadComponent: () =>
          import('./features/producto-detalle/view/producto-detalle').then(
            (m) => m.ProductoDetalle,
          ),
      },
    ],
  },
];