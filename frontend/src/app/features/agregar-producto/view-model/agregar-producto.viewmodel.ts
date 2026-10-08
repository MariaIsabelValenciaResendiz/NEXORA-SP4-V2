import { DestroyRef, inject, Injectable, signal } from '@angular/core';

import { FormBuilder, Validators } from '@angular/forms';

import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

import { ProductoService } from '../../../core/services/producto.service';
import { CrearProductoRequest } from '../../../core/models/crear-producto-request.model';

@Injectable()
export class AgregarProductoViewModel {
  private readonly formBuilder = inject(FormBuilder);
  private readonly productoService = inject(ProductoService);
  private readonly destroyRef = inject(DestroyRef);

  readonly cargando = signal(false);

  readonly mensajeExito = signal<string | null>(null);

  readonly mensajeError = signal<string | null>(null);

  readonly categorias = signal<string[]>([]);

  readonly formulario = this.formBuilder.nonNullable.group({
    titulo: ['', [Validators.required]],

    precio: [0, [Validators.required, Validators.min(0.01)]],

    categoria: ['', [Validators.required]],

    imagenUrl: ['', [Validators.required, Validators.pattern(/^https?:\/\/.+/i)]],

    descripcion: ['', [Validators.required]],
  });

  cargarCategorias(): void {
    this.productoService
      .obtenerCategorias()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (categorias) => {
          this.categorias.set(categorias);
        },

        error: () => {
          this.mensajeError.set('No fue posible cargar las categorías.');
        },
      });
  }

  crearProducto(): void {
    this.mensajeExito.set(null);
    this.mensajeError.set(null);

    if (this.formulario.invalid) {
      this.formulario.markAllAsTouched();
      return;
    }

    const datos = this.formulario.getRawValue();

    const request: CrearProductoRequest = {
      titulo: datos.titulo.trim(),
      precio: datos.precio,
      categoria: datos.categoria,
      imagenUrl: datos.imagenUrl.trim(),
      descripcion: datos.descripcion.trim(),
    };

    this.cargando.set(true);

    this.productoService
      .crear(request)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (productoCreado) => {
          this.mensajeExito.set(`Producto creado. ID: ${productoCreado.id}`);

          this.formulario.reset({
            titulo: '',
            precio: 0,
            categoria: '',
            imagenUrl: '',
            descripcion: '',
          });

          this.cargando.set(false);
        },

        error: () => {
          this.mensajeError.set('No fue posible crear el producto.');

          this.cargando.set(false);
        },
      });
  }
}
