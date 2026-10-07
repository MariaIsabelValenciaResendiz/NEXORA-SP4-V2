export interface ProductoCarritoHistorial {
  productId: number;
  quantity: number;
  title: string;
}

export interface CarritoHistorial {
  id: number;
  date: string;
  userId: number;
  products: ProductoCarritoHistorial[];
}