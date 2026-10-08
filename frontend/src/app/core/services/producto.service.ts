import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { CrearProductoRequest } from '../models/crear-producto-request.model';
import { ProductoCreado } from '../models/producto-creado.model';
import { API_URL } from '../constants/api.constants';
import { Producto } from '../models/producto.model';

@Injectable({ providedIn: 'root' })
export class ProductoService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = `${API_URL}/productos`;

  obtenerTodos(): Observable<Producto[]> {
    return this.http.get<Producto[]>(this.apiUrl);
  }

  obtenerPorId(id: number): Observable<Producto> {
    return this.http.get<Producto>(`${this.apiUrl}/${id}`);
  }

  obtenerCategorias(): Observable<string[]> {
    return this.http.get<string[]>(`${this.apiUrl}/categorias`);
  }

  obtenerPorCategoria(categoria: string): Observable<Producto[]> {
    const params = new HttpParams().set('categoria', categoria);

    return this.http.get<Producto[]>(`${this.apiUrl}/por-categoria`, { params });
  }
  crear(producto: CrearProductoRequest): Observable<ProductoCreado> {
    return this.http.post<ProductoCreado>(this.apiUrl, producto);
  }
}
