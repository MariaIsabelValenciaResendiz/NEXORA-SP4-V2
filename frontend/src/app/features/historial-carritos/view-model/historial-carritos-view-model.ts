import { HttpErrorResponse } from '@angular/common/http';
import { DestroyRef, Injectable, inject, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { finalize } from 'rxjs';

import { CarritoHistorial } from '../../../core/models/carrito-historial.model';
import { HistorialCarritosService } from '../../../core/services/historial-carritos.service';
import { SessionService } from '../../../core/services/session.service';

@Injectable()
export class HistorialCarritosViewModel {
  private readonly servicio = inject(HistorialCarritosService);
  private readonly sesion = inject(SessionService);
  private readonly destroyRef = inject(DestroyRef);
  private readonly usuario = this.sesion.obtener();

  readonly rol = this.usuario?.rol ?? 'Visitante';
  readonly contrasena = signal('');
  readonly carritos = signal<CarritoHistorial[]>([]);
  readonly cargando = signal(false);
  readonly consultado = signal(false);
  readonly error = signal('');
  readonly expandidoId = signal<number | null>(null);

  consultar(): void {
    if (this.cargando()) {
      return;
    }

    this.error.set('');

    if (!this.usuario || !['Administrador', 'Auditor'].includes(this.usuario.rol)) {
      this.error.set('No tienes permiso para consultar esta sección.');
      return;
    }

    const nombre = this.usuario.nombre;
    const contrasena = this.contrasena();

    if (!nombre.trim() || nombre.length > 100 || nombre.includes(':')
        || !contrasena.trim() || contrasena.length > 200) {
      this.error.set('Introduce una contraseña válida de hasta 200 caracteres.');
      return;
    }

    this.cargando.set(true);
    this.consultado.set(false);
    this.carritos.set([]);
    this.expandidoId.set(null);

    this.servicio.obtener(nombre, contrasena)
      .pipe(
        takeUntilDestroyed(this.destroyRef),
        finalize(() => this.cargando.set(false))
      )
      .subscribe({
        next: (carritos) => {
          this.carritos.set(carritos);
          this.consultado.set(true);
        },
        error: (error: HttpErrorResponse) => this.mostrarError(error)
      });
  }

  alternarDetalle(id: number): void {
    this.expandidoId.update((actual) => actual === id ? null : id);
  }

  private mostrarError(error: HttpErrorResponse): void {
    const mensajes: Record<number, string> = {
      0: 'No se pudo conectar con el backend. Comprueba que esté iniciado.',
      400: 'Revisa los datos introducidos.',
      401: 'La contraseña no es correcta. Vuelve a introducirla.',
      403: 'Solo Administrador y Auditor pueden consultar el historial.'
    };

    this.error.set(mensajes[error.status] ?? 'No se pudo consultar el historial. Inténtalo de nuevo.');
  }
}