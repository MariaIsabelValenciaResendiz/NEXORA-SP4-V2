import {
  Component,
  inject,
  OnInit
} from '@angular/core';

import { DecimalPipe } from '@angular/common';

import {
  FeedbackState
} from '../../../shared/components/feedback-state/feedback-state';

import {
  CatalogoProductosViewModel
} from '../view-model/catalogo-productos.viewmodel';

@Component({
  selector: 'app-catalogo-productos',
  standalone: true,

  imports: [
    DecimalPipe,
    FeedbackState
  ],

  providers: [
    CatalogoProductosViewModel
  ],

  templateUrl: './catalogo-productos.html',
  styleUrl: './catalogo-productos.scss'
})
export class CatalogoProductos implements OnInit {
  readonly viewModel =
    inject(CatalogoProductosViewModel);

  ngOnInit(): void {
    this.viewModel.cargarCategorias();
    this.viewModel.cargarProductos();
  }

  onImageError(event: Event): void {
    const imagen =
      event.target as HTMLImageElement;

    if (
      imagen.dataset['fallbackAplicado'] ===
      'true'
    ) {
      return;
    }

    imagen.dataset['fallbackAplicado'] =
      'true';

    imagen.src =
      '/images/producto-placeholder.svg';
  }
}
