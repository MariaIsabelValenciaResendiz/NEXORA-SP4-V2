import {
  HttpErrorResponse
} from '@angular/common/http';

import {
  DestroyRef,
  Injectable,
  inject,
  signal
} from '@angular/core';

import {
  ActivatedRoute,
  Router
} from '@angular/router';

import {
  takeUntilDestroyed
} from '@angular/core/rxjs-interop';

import {
  CarritoService
} from '../../../core/services/carrito.service';

import {
  ProductoService
} from '../../../core/services/producto.service';

import {
  Producto
} from '../../../core/models/producto.model';


export type RolDemo =
  'Cliente' |
  'Auditor' |
  'Administrador';


@Injectable()
export class ProductoDetalleViewModel {

  private readonly route =
    inject(ActivatedRoute);

  private readonly router =
    inject(Router);

  private readonly productos =
    inject(ProductoService);

  private readonly carrito =
    inject(CarritoService);

  private readonly destroyRef =
    inject(DestroyRef);


  readonly producto =
    signal<Producto | null>(null);

  readonly rol =
    signal<RolDemo>('Cliente');

  readonly categoriaOrigen =
    signal<string | null>(null);

  readonly cantidad =
    signal(1);

  readonly cargando =
    signal(true);

  readonly agregando =
    signal(false);

  readonly mensaje =
    signal('');

  readonly error =
    signal('');


  constructor() {

    this.route.queryParamMap
      .pipe(
        takeUntilDestroyed(
          this.destroyRef
        )
      )
      .subscribe(parametros => {

        const rolParametro =
          parametros
            .get('rol')
            ?.toLowerCase();

        if (
          rolParametro ===
          'administrador'
        ) {

          this.rol.set(
            'Administrador'
          );

        } else if (
          rolParametro ===
          'auditor'
        ) {

          this.rol.set(
            'Auditor'
          );

        } else {

          this.rol.set(
            'Cliente'
          );
        }


        this.categoriaOrigen.set(
          parametros.get('categoria')
        );

      });


    this.route.paramMap
      .pipe(
        takeUntilDestroyed(
          this.destroyRef
        )
      )
      .subscribe(parametros => {

        this.cargarProducto(
          Number(
            parametros.get('id')
          )
        );

      });
  }


  cargarProducto(
    id = Number(
      this.route.snapshot.paramMap
        .get('id')
    )
  ): void {

    this.cargando.set(true);
    this.error.set('');


    if (
      !Number.isInteger(id) ||
      id < 1
    ) {

      this.productoNoDisponible();
      return;
    }


    this.productos
      .obtenerPorId(id)
      .pipe(
        takeUntilDestroyed(
          this.destroyRef
        )
      )
      .subscribe({

        next: producto => {

          this.producto.set(
            producto
          );

          this.cargando.set(
            false
          );
        },


        error: (
          error: HttpErrorResponse
        ) => {

          console.error(
            'Error al consultar producto',
            error
          );

          this.productoNoDisponible();
        }

      });
  }


  volverAlCatalogo(): void {

    const categoria =
      this.categoriaOrigen();


    void this.router.navigate(
      ['/catalogo'],
      {
        queryParams: categoria
          ? { categoria }
          : {}
      }
    );
  }


  actualizarCantidad(
    evento: Event
  ): void {

    const valor = Number(
      (
        evento.target as
          HTMLInputElement
      ).value
    );


    this.cantidad.set(
      Number.isInteger(valor) &&
      valor > 0
        ? valor
        : 1
    );

    this.mensaje.set('');
  }


  disminuirCantidad(): void {

    this.cantidad.update(
      valor =>
        Math.max(
          1,
          valor - 1
        )
    );

    this.mensaje.set('');
  }


  aumentarCantidad(): void {

    this.cantidad.update(
      valor =>
        valor + 1
    );

    this.mensaje.set('');
  }


  agregarAlCarrito(): void {

    const producto =
      this.producto();

    const cantidad =
      this.cantidad();


    if (
      this.rol() !== 'Cliente' ||
      !producto ||
      !Number.isInteger(cantidad) ||
      cantidad < 1
    ) {
      return;
    }


    this.agregando.set(true);
    this.mensaje.set('');
    this.error.set('');


    this.carrito
      .agregar({
        clienteId: 2,
        productoId: producto.id,
        cantidad
      })
      .pipe(
        takeUntilDestroyed(
          this.destroyRef
        )
      )
      .subscribe({

        next: respuesta => {

          this.mensaje.set(
            respuesta.mensaje
          );

          this.agregando.set(
            false
          );
        },


        error: () => {

          this.error.set(
            'No se pudo añadir el producto. Intenta nuevamente.'
          );

          this.agregando.set(
            false
          );
        }

      });
  }


  private productoNoDisponible(): void {

    this.producto.set(null);

    this.cargando.set(false);


    window.alert(
      'Producto no disponible'
    );


    void this.router.navigate(
      ['/catalogo']
    );
  }
}
