import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';

import { API_URL } from '../constants/api.constants';
import { LoginRequest } from '../models/login-request.model';
import { Usuario } from '../models/usuario.model';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly http = inject(HttpClient);

  login(credenciales: LoginRequest): Observable<Usuario> {
    return this.http.post<Usuario>(`${API_URL}/auth/login`, credenciales);
  }
}