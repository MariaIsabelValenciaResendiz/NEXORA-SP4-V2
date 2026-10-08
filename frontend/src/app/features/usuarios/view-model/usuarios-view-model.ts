import { HttpErrorResponse } from '@angular/common/http';
import { DestroyRef, Injectable, inject, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { finalize } from 'rxjs';

import {
  MENSAJE_CREDENCIALES_INVALIDAS,
  MENSAJE_SIN_CONEXION
} from '../../../core/constants/mensajes.constants';

import { UsuarioDirectorio } from '../../../core/models/usuario-directorio.model';
import { SessionService } from '../../../core/services/session.service';
import { UsuariosService } from '../../../core/services/usuarios.service';

@Injectable()
export class UsuariosViewModel {
  private readonly servicio = inject(UsuariosService);
  private readonly usuario = inject(SessionService).obtener();
  private readonly destroyRef = inject(DestroyRef);

  readonly rol = this.usuario?.rol ?? 'Visitante';
  readonly contrasena = signal('');
  readonly usuarios = signal<UsuarioDirectorio[]>([]);
  readonly cargando = signal(false);
  readonly consultado = signal(false);
  readonly error = signal('');
  readonly expandidoId = signal<number | null>(null);

  consultar(): void {
    if (this.cargando()) {
      return;
    }

    this.error.set('');

    if (
      !this.usuario ||
      !['Administrador', 'Auditor'].includes(this.usuario.rol)
    ) {
      this.error.set('No tienes permiso para consultar usuarios.');
      return;
    }

    const nombre = this.usuario.nombre;
    const contrasena = this.contrasena();

    if (
      !nombre.trim() ||
      nombre.length > 100 ||
      nombre.includes(':') ||
      !contrasena.trim() ||
      contrasena.length > 200
    ) {
      this.error.set(
        'Introduce una contraseña válida de hasta 200 caracteres.'
      );
      return;
    }

    this.cargando.set(true);
    this.consultado.set(false);
    this.usuarios.set([]);
    this.expandidoId.set(null);

    this.servicio.obtener(nombre, contrasena)
      .pipe(
        takeUntilDestroyed(this.destroyRef),
        finalize(() => this.cargando.set(false))
      )
      .subscribe({
        next: (usuarios) => {
          this.usuarios.set(usuarios);
          this.consultado.set(true);
        },
        error: (error: HttpErrorResponse) => this.mostrarError(error)
      });
  }

  alternarDetalle(id: number): void {
    this.expandidoId.update(
      (actual) => actual === id ? null : id
    );
  }

  private mostrarError(error: HttpErrorResponse): void {
    const mensajes: Record<number, string> = {
      0: MENSAJE_SIN_CONEXION,
      400: 'Revisa los datos introducidos.',
      401: MENSAJE_CREDENCIALES_INVALIDAS,
      403: 'Solo Administrador y Auditor pueden consultar usuarios.'
    };

    this.error.set(
      mensajes[error.status] ??
      'La consulta se interrumpió. Inténtalo de nuevo.'
    );
  }
}