import { Component, Input } from '@angular/core';

export type StatusMessageType = 'success' | 'error' | 'info';

@Component({
  selector: 'app-status-message',
  imports: [],
  templateUrl: './status-message.html',
  styleUrl: './status-message.scss'
})
export class StatusMessage {
  @Input() message = '';
  @Input() type: StatusMessageType = 'success';
}