import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Producto } from '../models/producto.model';

@Injectable({
  providedIn: 'root'
})
export class ProductoService {
  private readonly http = inject(HttpClient);

  private readonly apiUrl = 'http://localhost:5081/api/productos';

  obtenerTodos(): Observable<Producto[]> {
    return this.http.get<Producto[]>(this.apiUrl);
  }

  obtenerCategorias(): Observable<string[]> {
    return this.http.get<string[]>(
      `${this.apiUrl}/categorias`
    );
  }

  obtenerPorCategoria(categoria: string): Observable<Producto[]> {
    const params = new HttpParams()
      .set('categoria', categoria);

    return this.http.get<Producto[]>(
      `${this.apiUrl}/por-categoria`,
      { params }
    );
  }
}
