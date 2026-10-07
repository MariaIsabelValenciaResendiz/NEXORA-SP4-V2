import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable, timeout } from 'rxjs';

import { API_URL } from '../constants/api.constants';
import { UsuarioDirectorio } from '../models/usuario-directorio.model';

@Injectable({ providedIn: 'root' })
export class UsuariosService {
  private readonly http = inject(HttpClient);

  obtener(nombre: string, contrasena: string): Observable<UsuarioDirectorio[]> {
    const bytes = new TextEncoder().encode(`${nombre}:${contrasena}`);

    const texto = Array.from(
      bytes,
      (byte) => String.fromCharCode(byte)
    ).join('');

    const headers = new HttpHeaders({
      Authorization: `Basic ${btoa(texto)}`
    });

    return this.http
      .get<UsuarioDirectorio[]>(`${API_URL}/users`, { headers })
      .pipe(timeout(10000));
  }
}