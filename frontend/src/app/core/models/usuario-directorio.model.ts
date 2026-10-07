import { Usuario } from './usuario.model';

export interface CoordenadasUsuario {
  latitud: string;
  longitud: string;
}

export interface DireccionUsuario {
  ciudad: string;
  calle: string;
  numero: number;
  codigoPostal: string;
  geolocalizacion: CoordenadasUsuario;
}

export interface UsuarioDirectorio extends Usuario {
  nombreCompleto: string;
  correo: string;
  telefono: string;
  direccion: DireccionUsuario;
}