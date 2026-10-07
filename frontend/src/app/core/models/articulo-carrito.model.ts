export interface AgregarArticuloRequest {
  clienteId: number;
  productoId: number;
  cantidad: number;
}

export interface AgregarArticuloResponse {
  productoId: number;
  nombre: string;
  precio: number;
  cantidad: number;
  mensaje: string;
}
