import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';

import { FeedbackState } from '../../../shared/components/feedback-state/feedback-state';
import { StatusMessage } from '../../../shared/components/status-message/status-message';
import { ProductoDetalleViewModel } from '../view-model/producto-detalle-view-model';

@Component({
  selector: 'app-producto-detalle',
  imports: [CommonModule, FeedbackState, StatusMessage],
  providers: [ProductoDetalleViewModel],
  templateUrl: './producto-detalle.html',
  styleUrl: './producto-detalle.scss'
})
export class ProductoDetalle {
  readonly vm = inject(ProductoDetalleViewModel);
}
