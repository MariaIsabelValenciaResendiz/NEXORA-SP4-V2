import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { API_URL } from '../constants/api.constants';
import {
  ActualizarCantidadRequest,
  AgregarArticuloRequest,
  AgregarArticuloResponse,
  ArticuloCarrito
} from '../models/articulo-carrito.model';

@Injectable({ providedIn: 'root' })
export class CarritoService {
  private readonly http = inject(HttpClient);

  agregar(request: AgregarArticuloRequest): Observable<AgregarArticuloResponse> {
    return this.http.post<AgregarArticuloResponse>(`${API_URL}/carrito`, request);
  }

  obtenerPorCliente(clienteId: number): Observable<ArticuloCarrito[]> {
    return this.http.get<ArticuloCarrito[]>(`${API_URL}/carrito/${clienteId}`);
  }

  actualizarCantidad(
    clienteId: number,
    productoId: number,
    request: ActualizarCantidadRequest
  ): Observable<ArticuloCarrito> {
    return this.http.put<ArticuloCarrito>(
      `${API_URL}/carrito/${clienteId}/articulos/${productoId}`,
      request
    );
  }

  eliminar(clienteId: number, productoId: number): Observable<void> {
    return this.http.delete<void>(`${API_URL}/carrito/${clienteId}/articulos/${productoId}`);
  }
}
