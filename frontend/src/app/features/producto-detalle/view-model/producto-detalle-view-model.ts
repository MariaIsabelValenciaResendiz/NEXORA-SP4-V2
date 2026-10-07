import { HttpErrorResponse } from '@angular/common/http';
import { DestroyRef, Injectable, inject, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

import { CarritoService } from '../../../core/services/carrito.service';
import { ProductoService } from '../../../core/services/producto.service';
import { Producto } from '../../../core/models/producto.model';

export type RolDemo = 'Cliente' | 'Auditor';

@Injectable()
export class ProductoDetalleViewModel {
  private readonly route = inject(ActivatedRoute);
  private readonly productos = inject(ProductoService);
  private readonly carrito = inject(CarritoService);
  private readonly destroyRef = inject(DestroyRef);

  readonly producto = signal<Producto | null>(null);
  readonly rol = signal<RolDemo>('Cliente');
  readonly cantidad = signal(1);
  readonly cargando = signal(true);
  readonly agregando = signal(false);
  readonly mensaje = signal('');
  readonly error = signal('');

  constructor() {
    this.route.queryParamMap
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(parametros => {
        this.rol.set(parametros.get('rol')?.toLowerCase() === 'auditor' ? 'Auditor' : 'Cliente');
      });

    this.route.paramMap
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe(parametros => this.cargarProducto(Number(parametros.get('id'))));
  }

  cargarProducto(id = Number(this.route.snapshot.paramMap.get('id'))): void {
    this.cargando.set(true);
    this.error.set('');

    if (!Number.isInteger(id) || id < 1) {
      this.error.set('El identificador del producto no es válido.');
      this.cargando.set(false);
      return;
    }

    this.productos.obtenerPorId(id)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: producto => {
          this.producto.set(producto);
          this.cargando.set(false);
        },
        error: (error: HttpErrorResponse) => {
          this.error.set(error.status === 400
            ? 'No encontramos ese producto.'
            : 'No pudimos cargar el producto. Revisa tu conexión e intenta nuevamente.');
          this.cargando.set(false);
        }
      });
  }

  actualizarCantidad(evento: Event): void {
    const valor = Number((evento.target as HTMLInputElement).value);
    this.cantidad.set(Number.isInteger(valor) && valor > 0 ? valor : 1);
    this.mensaje.set('');
  }

  disminuirCantidad(): void {
    this.cantidad.update(valor => Math.max(1, valor - 1));
    this.mensaje.set('');
  }

  aumentarCantidad(): void {
    this.cantidad.update(valor => valor + 1);
    this.mensaje.set('');
  }

  agregarAlCarrito(): void {
    const producto = this.producto();
    const cantidad = this.cantidad();

    if (this.rol() !== 'Cliente' || !producto || !Number.isInteger(cantidad) || cantidad < 1) {
      return;
    }

    this.agregando.set(true);
    this.mensaje.set('');
    this.error.set('');

    this.carrito.agregar({ clienteId: 2, productoId: producto.id, cantidad })
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: respuesta => {
          this.mensaje.set(respuesta.mensaje);
          this.agregando.set(false);
        },
        error: () => {
          this.error.set('No se pudo añadir el producto. Intenta nuevamente.');
          this.agregando.set(false);
        }
      });
  }
}
