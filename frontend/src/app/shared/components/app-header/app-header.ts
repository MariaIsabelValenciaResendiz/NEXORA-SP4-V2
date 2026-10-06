import { Component, Input } from '@angular/core';

export type HeaderRole =
  | 'Cliente'
  | 'Administrador'
  | 'Auditor'
  | 'Visitante';

@Component({
  selector: 'app-header',
  imports: [],
  templateUrl: './app-header.html',
  styleUrl: './app-header.scss'
})
export class AppHeader {
  @Input() role: HeaderRole | null = null;
}