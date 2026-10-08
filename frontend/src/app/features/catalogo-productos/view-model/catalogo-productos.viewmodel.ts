import {
  DestroyRef,
  inject,
  Injectable,
  signal
} from '@angular/core';

import {
  finalize,
  Subscription
} from 'rxjs';

import {
  takeUntilDestroyed
} from '@angular/core/rxjs-interop';

import { Producto } from '../../../core/models/producto.model';
import { ProductoService } from '../../../core/services/producto.service';

@Injectable()
export class CatalogoProductosViewModel {
  private readonly productoService = inject(ProductoService);
  private readonly destroyRef = inject(DestroyRef);

  private solicitudProductos?: Subscription;
  private numeroSolicitudProductos = 0;

  readonly productos = signal<Producto[]>([]);

  readonly categorias = signal<string[]>([]);

  readonly categoriaSeleccionada =
    signal<string | null>(null);

  readonly cargando = signal(false);

  readonly error =
    signal<string | null>(null);

  readonly cargandoCategorias =
    signal(false);

  readonly errorCategorias =
    signal<string | null>(null);

  readonly descripcionesExpandidas =
    signal<ReadonlySet<number>>(new Set<number>());

  cargarCategorias(): void {
    if (this.cargandoCategorias()) {
      return;
    }

    this.cargandoCategorias.set(true);
    this.errorCategorias.set(null);

    this.productoService
      .obtenerCategorias()
      .pipe(
        takeUntilDestroyed(this.destroyRef),
        finalize(() =>
          this.cargandoCategorias.set(false)
        )
      )
      .subscribe({
        next: (categorias) => {
          this.categorias.set(categorias);
        },
        error: () => {
          this.categorias.set([]);

          this.errorCategorias.set(
            'No fue posible cargar las categorías. Intenta nuevamente.'
          );
        }
      });
  }

  cargarProductos(): void {
    this.categoriaSeleccionada.set(null);
    this.consultarProductos(null);
  }

  seleccionarCategoria(categoria: string): void {
    const categoriaNormalizada = categoria.trim();

    if (!categoriaNormalizada) {
      return;
    }

    this.categoriaSeleccionada.set(
      categoriaNormalizada
    );

    this.consultarProductos(
      categoriaNormalizada
    );
  }

  verTodos(): void {
    this.categoriaSeleccionada.set(null);
    this.consultarProductos(null);
  }

  reintentar(): void {
    this.consultarProductos(
      this.categoriaSeleccionada()
    );
  }

  reintentarCategorias(): void {
    this.cargarCategorias();
  }

  descripcionEsLarga(descripcion: string): boolean {
    return descripcion.length > 100;
  }

  estaDescripcionExpandida(
    idProducto: number
  ): boolean {
    return this.descripcionesExpandidas()
      .has(idProducto);
  }

  alternarDescripcion(
    idProducto: number
  ): void {
    const actualizadas =
      new Set(
        this.descripcionesExpandidas()
      );

    if (actualizadas.has(idProducto)) {
      actualizadas.delete(idProducto);
    } else {
      actualizadas.add(idProducto);
    }

    this.descripcionesExpandidas.set(
      actualizadas
    );
  }

  private consultarProductos(
    categoria: string | null
  ): void {
    const numeroSolicitud =
      ++this.numeroSolicitudProductos;

    this.solicitudProductos?.unsubscribe();

    this.productos.set([]);
    this.error.set(null);
    this.cargando.set(true);

    this.descripcionesExpandidas.set(
      new Set<number>()
    );

    const consulta$ =
      categoria === null
        ? this.productoService.obtenerTodos()
        : this.productoService.obtenerPorCategoria(
            categoria
          );

    this.solicitudProductos =
      consulta$
        .pipe(
          takeUntilDestroyed(
            this.destroyRef
          )
        )
        .subscribe({
          next: (productos) => {
            if (
              numeroSolicitud !==
              this.numeroSolicitudProductos
            ) {
              return;
            }

            this.productos.set(productos);
          },

          error: () => {
            if (
              numeroSolicitud !==
              this.numeroSolicitudProductos
            ) {
              return;
            }

            this.productos.set([]);

            this.error.set(
              categoria === null
                ? 'No fue posible cargar los productos. Intenta nuevamente.'
                : `No fue posible cargar los productos de ${categoria}. Intenta nuevamente.`
            );

            this.cargando.set(false);
          },

          complete: () => {
            if (
              numeroSolicitud !==
              this.numeroSolicitudProductos
            ) {
              return;
            }

            this.cargando.set(false);
          }
        });
  }
}