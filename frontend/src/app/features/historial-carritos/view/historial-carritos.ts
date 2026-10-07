import { DatePipe } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { FeedbackState } from '../../../shared/components/feedback-state/feedback-state';
import { HistorialCarritosViewModel } from '../view-model/historial-carritos-view-model';

@Component({
  selector: 'app-historial-carritos',
  imports: [DatePipe, FormsModule, FeedbackState],
  providers: [HistorialCarritosViewModel],
  templateUrl: './historial-carritos.html',
  styleUrl: './historial-carritos.scss'
})
export class HistorialCarritos {
  readonly vm = inject(HistorialCarritosViewModel);
}