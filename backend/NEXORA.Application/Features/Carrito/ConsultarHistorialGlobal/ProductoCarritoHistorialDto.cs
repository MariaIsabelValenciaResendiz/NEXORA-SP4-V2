namespace NEXORA.Application.Features.Carrito.ConsultarHistorialGlobal;

public sealed record ProductoCarritoHistorialDto(
    int ProductId,
    int Quantity,
    string Title);