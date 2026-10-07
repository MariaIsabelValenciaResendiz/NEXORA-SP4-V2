import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';

import { API_URL } from '../constants/api.constants';
import { CarritoHistorial } from '../models/carrito-historial.model';

@Injectable({ providedIn: 'root' })
export class HistorialCarritosService {
  private readonly http = inject(HttpClient);

  obtener(nombre: string, contrasena: string): Observable<CarritoHistorial[]> {
    const bytes = new TextEncoder().encode(`${nombre}:${contrasena}`);
    const texto = Array.from(bytes, (byte) => String.fromCharCode(byte)).join('');
    const headers = new HttpHeaders({ Authorization: `Basic ${btoa(texto)}` });

    return this.http.get<CarritoHistorial[]>(`${API_URL}/carts`, { headers });
  }
}