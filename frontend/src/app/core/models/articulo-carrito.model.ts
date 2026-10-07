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

export interface ArticuloCarrito {
  productoId: number;
  nombre: string;
  precio: number;
  imagenUrl: string;
  cantidad: number;
  subtotal: number;
}

export interface ActualizarCantidadRequest {
  cantidad: number;
}
