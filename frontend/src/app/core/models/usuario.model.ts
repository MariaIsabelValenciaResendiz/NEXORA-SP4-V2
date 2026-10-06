export type RolUsuario = 'Cliente' | 'Administrador' | 'Auditor';

export interface Usuario {
  id: number;
  nombre: string;
  rol: RolUsuario;
}