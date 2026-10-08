import { Injectable, inject } from '@angular/core';
import { Router } from '@angular/router';

import { SessionService } from '../../../core/services/session.service';

@Injectable()
export class CuentaViewModel {
  private readonly sesion = inject(SessionService);
  private readonly router = inject(Router);
  private readonly usuario = this.sesion.obtener();

  readonly nombre = this.usuario?.nombreCompleto || this.usuario?.nombre || '';
  readonly correo = this.usuario?.correo ?? '';
  readonly rol = this.usuario?.rol ?? '';

  readonly iniciales = this.nombre
    .trim()
    .split(/\s+/)
    .filter(Boolean)
    .slice(0, 2)
    .map((palabra) => palabra[0].toUpperCase())
    .join('');

  cerrarSesion(): void {
    this.sesion.limpiar();
    this.router.navigate(['/auth/login'], {
      queryParams: { sesion: 'cerrada' },
      replaceUrl: true
    });
  }
}