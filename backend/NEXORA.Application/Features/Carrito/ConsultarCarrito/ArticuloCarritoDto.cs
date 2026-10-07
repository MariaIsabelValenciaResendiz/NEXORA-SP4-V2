namespace NEXORA.Application.Features.Carrito.ConsultarCarrito;

public sealed record ArticuloCarritoDto(
    int ProductoId,
    string Nombre,
    decimal Precio,
    string ImagenUrl,
    int Cantidad,
    decimal Subtotal);
