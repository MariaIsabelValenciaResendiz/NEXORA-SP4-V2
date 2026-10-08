import { Routes } from '@angular/router';
import { usuariosGuard } from '../../core/guards/usuarios.guard';

export const USUARIOS_ROUTES: Routes = [
  {
    path: '',
    canActivate: [usuariosGuard],
    loadComponent: () =>
      import('./view/usuarios').then((m) => m.Usuarios)
  }
];