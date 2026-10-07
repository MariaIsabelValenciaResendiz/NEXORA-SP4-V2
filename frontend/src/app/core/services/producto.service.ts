import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
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
}
//Este servicio hace una sola cosa: consultar el backend de productos. 
// No contiene lógica de vista, no filtra, no transforma datos y no duplica productos.