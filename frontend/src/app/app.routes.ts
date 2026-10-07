import { Routes } from '@angular/router';

import { AuthLayout } from './layouts/auth-layout/auth-layout';
import { MainLayout } from './layouts/main-layout/main-layout';

export const routes: Routes = [
  {
    path: 'auth',
    component: AuthLayout,
    children: []
  },
  {
    path: '',
    component: MainLayout,
    children: [
      {
        path: 'catalogo',
        loadComponent: () =>
          import(
            './features/catalogo-productos/view/catalogo-productos'
          ).then((m) => m.CatalogoProductos)
      }
    ]
  }
];
