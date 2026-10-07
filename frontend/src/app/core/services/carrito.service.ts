import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { API_URL } from '../constants/api.constants';
import {
  AgregarArticuloRequest,
  AgregarArticuloResponse
} from '../models/articulo-carrito.model';

@Injectable({ providedIn: 'root' })
export class CarritoService {
  private readonly http = inject(HttpClient);

  agregar(request: AgregarArticuloRequest): Observable<AgregarArticuloResponse> {
    return this.http.post<AgregarArticuloResponse>(`${API_URL}/carrito`, request);
  }
}
