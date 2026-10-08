import { Component, inject, OnInit } from '@angular/core';

import { ReactiveFormsModule } from '@angular/forms';

import { StatusMessage } from '../../../shared/components/status-message/status-message';

import { AgregarProductoViewModel } from '../view-model/agregar-producto.viewmodel';

@Component({
  selector: 'app-agregar-producto',

  imports: [ReactiveFormsModule, StatusMessage],

  providers: [AgregarProductoViewModel],

  templateUrl: './agregar-producto.html',
  styleUrl: './agregar-producto.scss',
})
export class AgregarProducto implements OnInit {
  protected readonly vm = inject(AgregarProductoViewModel);

  ngOnInit(): void {
    this.vm.cargarCategorias();
  }

  protected alEnviar(): void {
    this.vm.crearProducto();
  }
}
