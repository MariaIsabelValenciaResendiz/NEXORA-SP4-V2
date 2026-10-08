import { HttpErrorResponse } from '@angular/common/http';
import { Injectable, computed, inject, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import {
  MENSAJE_CREDENCIALES_INVALIDAS,
  MENSAJE_ERROR_GENERAL,
  MENSAJE_SESION_CERRADA,
  MENSAJE_SIN_CONEXION
} from '../../../core/constants/mensajes.constants';
import { AuthService } from '../../../core/services/auth.service';
import { SessionService } from '../../../core/services/session.service';
import { StatusMessageType } from '../../../shared/components/status-message/status-message';

@Injectable()
export class LoginViewModel {
  private readonly auth = inject(AuthService);
  private readonly sesion = inject(SessionService);
  private readonly router = inject(Router);
  private readonly ruta = inject(ActivatedRoute);

  readonly nombre = signal('');
  readonly contrasena = signal('');
  readonly cargando = signal(false);
  readonly mensaje = signal('');
  readonly tipoMensaje = signal<StatusMessageType>('error');

  private readonly intentado = signal(false);
  private readonly credencialesInvalidas = signal(false);

  readonly nombreInvalido = computed(
    () => this.credencialesInvalidas() || (this.intentado() && this.nombre().trim() === '')
  );

  readonly contrasenaInvalida = computed(
    () => this.credencialesInvalidas() || (this.intentado() && this.contrasena() === '')
  );

  constructor() {
    if (this.ruta.snapshot.queryParamMap.get('sesion') === 'cerrada') {
      this.tipoMensaje.set('success');
      this.mensaje.set(MENSAJE_SESION_CERRADA);
    }
  }

  iniciarSesion(): void {
    this.intentado.set(true);
    this.credencialesInvalidas.set(false);
    this.mensaje.set('');

    if (this.nombreInvalido() || this.contrasenaInvalida()) {
      return;
    }

    this.cargando.set(true);

    this.auth
      .login({ nombre: this.nombre().trim(), contrasena: this.contrasena() })
      .subscribe({
        next: (usuario) => {
          this.sesion.guardar(usuario);
          this.cargando.set(false);
          this.router.navigateByUrl('/');
        },
        error: (error: HttpErrorResponse) => {
          this.cargando.set(false);
          this.mostrarError(error);
        }
      });
  }

  private mostrarError(error: HttpErrorResponse): void {
    this.tipoMensaje.set('error');

    if (error.status === 0) {
      this.mensaje.set(MENSAJE_SIN_CONEXION);
    } else if (error.status === 401) {
      this.credencialesInvalidas.set(true);
      this.mensaje.set(MENSAJE_CREDENCIALES_INVALIDAS);
    } else {
      this.mensaje.set(MENSAJE_ERROR_GENERAL);
    }
  }
}