import { HttpErrorResponse } from '@angular/common/http';
import { computed, DestroyRef, inject, Injectable, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ActivatedRoute } from '@angular/router';

import { ArticuloCarrito } from '../../../core/models/articulo-carrito.model';
import { CarritoService } from '../../../core/services/carrito.service';

export type RolCarrito = 'Cliente' | 'Auditor';

@Injectable()
export class CarritoViewModel {
  private readonly route = inject(ActivatedRoute);
  private readonly carrito = inject(CarritoService);
  private readonly destroyRef = inject(DestroyRef);
  private readonly clienteId = 2;

  readonly articulos = signal<ArticuloCarrito[]>([]);
  readonly rol = signal<RolCarrito>('Cliente');
  readonly cargando = signal(true);
  readonly actualizandoProductoId = signal<number | null>(null);
  readonly error = signal('');
  readonly mensaje = signal('');
  readonly total = computed(() =>
    this.articulos().reduce((suma, articulo) => suma + articulo.precio * articulo.cantidad, 0)
  );
  readonly carritoVacio = computed(() => this.articulos().length === 0);

  constructor() {
    this.route.queryParamMap
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(parametros => {
        this.rol.set(parametros.get('rol')?.toLowerCase() === 'auditor' ? 'Auditor' : 'Cliente');
      });

    this.cargar();
  }

  cargar(): void {
    this.cargando.set(true);
    this.error.set('');

    this.carrito.obtenerPorCliente(this.clienteId)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: articulos => {
          this.articulos.set(articulos);
          this.cargando.set(false);
        },
        error: (error: HttpErrorResponse) => {
          this.error.set(error.status === 0
            ? 'No pudimos conectar con NEXORA. Revisa que el backend esté iniciado.'
            : 'No se pudo cargar el carrito. Intenta nuevamente.');
          this.cargando.set(false);
        }
      });
  }

  aumentar(articulo: ArticuloCarrito): void {
    this.cambiarCantidad(articulo, articulo.cantidad + 1);
  }

  disminuir(articulo: ArticuloCarrito): void {
    const nuevaCantidad = articulo.cantidad - 1;
    if (nuevaCantidad <= 0) {
      this.eliminar(articulo);
      return;
    }

    this.cambiarCantidad(articulo, nuevaCantidad);
  }

  cambiarCantidad(articulo: ArticuloCarrito, cantidad: number): void {
    if (this.rol() !== 'Cliente' || !Number.isInteger(cantidad) || cantidad < 1) {
      return;
    }

    this.iniciarActualizacion(articulo.productoId);
    this.carrito.actualizarCantidad(this.clienteId, articulo.productoId, { cantidad })
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: actualizado => {
          this.articulos.update(articulos => articulos.map(item =>
            item.productoId === actualizado.productoId ? actualizado : item
          ));
          this.mensaje.set('Cantidad actualizada.');
          this.finalizarActualizacion();
        },
        error: () => this.manejarError('No se pudo actualizar la cantidad.')
      });
  }

  eliminar(articulo: ArticuloCarrito): void {
    if (this.rol() !== 'Cliente') {
      return;
    }

    this.iniciarActualizacion(articulo.productoId);
    this.carrito.eliminar(this.clienteId, articulo.productoId)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: () => {
          this.articulos.update(articulos => articulos.filter(
            item => item.productoId !== articulo.productoId
          ));
          this.mensaje.set('Artículo eliminado del carrito.');
          this.finalizarActualizacion();
        },
        error: () => this.manejarError('No se pudo eliminar el artículo.')
      });
  }

  private iniciarActualizacion(productoId: number): void {
    this.actualizandoProductoId.set(productoId);
    this.error.set('');
    this.mensaje.set('');
  }

  private finalizarActualizacion(): void {
    this.actualizandoProductoId.set(null);
  }

  private manejarError(mensaje: string): void {
    this.error.set(mensaje);
    this.finalizarActualizacion();
  }
}
