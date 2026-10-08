import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { FeedbackState } from '../../../shared/components/feedback-state/feedback-state';
import { StatusMessage } from '../../../shared/components/status-message/status-message';
import { UsuariosViewModel } from '../view-model/usuarios-view-model';

@Component({
  selector: 'app-usuarios',
  imports: [FormsModule, FeedbackState, StatusMessage],
  providers: [UsuariosViewModel],
  templateUrl: './usuarios.html',
  styleUrl: './usuarios.scss'
})
export class Usuarios {
  readonly vm = inject(UsuariosViewModel);
}