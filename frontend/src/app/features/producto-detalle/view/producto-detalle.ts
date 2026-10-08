import {
  CommonModule
} from '@angular/common';

import {
  Component,
  inject
} from '@angular/core';

import {
  RouterLink
} from '@angular/router';

import {
  FeedbackState
} from '../../../shared/components/feedback-state/feedback-state';

import {
  StatusMessage
} from '../../../shared/components/status-message/status-message';

import {
  ProductoDetalleViewModel
} from '../view-model/producto-detalle-view-model';


@Component({
  selector: 'app-producto-detalle',

  imports: [
    CommonModule,
    FeedbackState,
    RouterLink,
    StatusMessage
  ],

  providers: [
    ProductoDetalleViewModel
  ],

  templateUrl: './producto-detalle.html',
  styleUrl: './producto-detalle.scss'
})
export class ProductoDetalle {

  readonly vm =
    inject(ProductoDetalleViewModel);


  onImageError(
    event: Event
  ): void {

    const imagen =
      event.target as HTMLImageElement;


    if (
      imagen.dataset[
        'fallbackAplicado'
      ] === 'true'
    ) {
      return;
    }


    imagen.dataset[
      'fallbackAplicado'
    ] = 'true';


    imagen.src =
      '/images/producto-placeholder.svg';
  }
}
