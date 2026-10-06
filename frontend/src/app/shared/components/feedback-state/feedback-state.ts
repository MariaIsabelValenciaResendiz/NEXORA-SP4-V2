import { Component, EventEmitter, Input, Output } from '@angular/core';

export type FeedbackTone = 'neutral' | 'error';

@Component({
  selector: 'app-feedback-state',
  imports: [],
  templateUrl: './feedback-state.html',
  styleUrl: './feedback-state.scss'
})
export class FeedbackState {
  @Input() icon = '!';
  @Input() title = '';
  @Input() message = '';
  @Input() actionLabel = '';
  @Input() tone: FeedbackTone = 'neutral';

  @Output() actionSelected = new EventEmitter<void>();

  onAction(): void {
    this.actionSelected.emit();
  }
}