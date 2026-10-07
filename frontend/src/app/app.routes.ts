import { Routes } from '@angular/router';

import { sesionActivaGuard } from './core/guards/sesion-activa.guard';
import { AuthLayout } from './layouts/auth-layout/auth-layout';
import { MainLayout } from './layouts/main-layout/main-layout';

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
    component: MainLayout,
    canActivate: [sesionActivaGuard],
    children: []
  }
];