namespace NEXORA.Application.Features.Carrito.ConsultarHistorialGlobal;

public sealed record CarritoHistorialDto(
    int Id,
    DateTimeOffset Date,
    int UserId,
    IReadOnlyList<ProductoCarritoHistorialDto> Products);