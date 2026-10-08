import { Component, inject } from '@angular/core';

import { CuentaViewModel } from '../view-model/cuenta.view-model';

@Component({
  selector: 'app-cuenta',
  providers: [CuentaViewModel],
  templateUrl: './cuenta.html',
  styleUrl: './cuenta.scss'
})
export class Cuenta {
  protected readonly vm = inject(CuentaViewModel);
}