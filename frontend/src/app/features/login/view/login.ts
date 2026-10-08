import { Component, inject } from '@angular/core';

import { StatusMessage } from '../../../shared/components/status-message/status-message';
import { LoginViewModel } from '../view-model/login.view-model';

@Component({
  selector: 'app-login',
  imports: [StatusMessage],
  providers: [LoginViewModel],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class Login {
  protected readonly vm = inject(LoginViewModel);

  protected alEnviar(evento: Event): void {
    evento.preventDefault();
    this.vm.iniciarSesion();
  }
}