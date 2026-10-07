namespace NEXORA.Application.Features.Carrito.AgregarArticulo;

public sealed record AgregarArticuloResponse(
    int ProductoId,
    string Nombre,
    decimal Precio,
    int Cantidad,
    string Mensaje);
