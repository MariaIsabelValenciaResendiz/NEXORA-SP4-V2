import { Injectable } from '@angular/core';

import { SESSION_USER_KEY } from '../constants/session.constants';
import { Usuario } from '../models/usuario.model';

@Injectable({ providedIn: 'root' })
export class SessionService {
  guardar(usuario: Usuario): void {
    localStorage.setItem(SESSION_USER_KEY, JSON.stringify(usuario));
  }

  obtener(): Usuario | null {
    const valor = localStorage.getItem(SESSION_USER_KEY);
    return valor ? (JSON.parse(valor) as Usuario) : null;
  }

  limpiar(): void {
    localStorage.removeItem(SESSION_USER_KEY);
  }
}