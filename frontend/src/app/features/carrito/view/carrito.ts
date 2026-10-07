import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';

import { FeedbackState } from '../../../shared/components/feedback-state/feedback-state';
import { StatusMessage } from '../../../shared/components/status-message/status-message';
import { CarritoViewModel } from '../view-model/carrito-view-model';

@Component({
  selector: 'app-carrito',
  imports: [CommonModule, FeedbackState, RouterLink, StatusMessage],
  providers: [CarritoViewModel],
  templateUrl: './carrito.html',
  styleUrl: './carrito.scss'
})
export class Carrito {
  readonly vm = inject(CarritoViewModel);
}
