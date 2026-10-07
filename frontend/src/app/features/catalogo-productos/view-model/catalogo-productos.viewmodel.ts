import { DestroyRef, inject, Injectable, signal } from '@angular/core';
import { finalize } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

import { Producto } from '../../../core/models/producto.model';
import { ProductoService } from '../../../core/services/producto.service';

@Injectable()
export class CatalogoProductosViewModel {
  private readonly productoService = inject(ProductoService);
  private readonly destroyRef = inject(DestroyRef);

  readonly productos = signal<Producto[]>([]);
  readonly cargando = signal(false);
  readonly error = signal<string | null>(null);

  readonly descripcionesExpandidas =
    signal<ReadonlySet<number>>(new Set<number>());

  cargarProductos(): void {
    if (this.cargando()) {
      return;
    }

    this.cargando.set(true);
    this.error.set(null);

    this.productoService
      .obtenerTodos()
      .pipe(
        takeUntilDestroyed(this.destroyRef),
        finalize(() => this.cargando.set(false))
      )
      .subscribe({
        next: (productos) => {
          this.productos.set(productos);
          this.descripcionesExpandidas.set(new Set<number>());
        },
        error: () => {
          this.productos.set([]);
          this.error.set(
            'No fue posible cargar los productos. Intenta nuevamente.'
          );
        }
      });
  }

  reintentar(): void {
    this.cargarProductos();
  }

  descripcionEsLarga(descripcion: string): boolean {
    return descripcion.length > 100;
  }

  estaDescripcionExpandida(idProducto: number): boolean {
    return this.descripcionesExpandidas().has(idProducto);
  }

  alternarDescripcion(idProducto: number): void {
    const actualizadas = new Set(this.descripcionesExpandidas());

    if (actualizadas.has(idProducto)) {
      actualizadas.delete(idProducto);
    } else {
      actualizadas.add(idProducto);
    }

    this.descripcionesExpandidas.set(actualizadas);
  }
}
