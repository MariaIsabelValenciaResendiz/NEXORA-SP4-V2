import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { API_URL } from '../constants/api.constants';
import { Producto } from '../models/producto.model';

@Injectable({ providedIn: 'root' })
export class ProductoService {
  private readonly http = inject(HttpClient);

  obtenerPorId(id: number): Observable<Producto> {
    return this.http.get<Producto>(`${API_URL}/productos/${id}`);
  }
}
