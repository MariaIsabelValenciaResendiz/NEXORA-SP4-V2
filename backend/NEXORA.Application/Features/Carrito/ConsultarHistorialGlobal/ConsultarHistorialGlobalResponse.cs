namespace NEXORA.Application.Features.Carrito.ConsultarHistorialGlobal;

public sealed record ConsultarHistorialGlobalResponse(
    EstadoConsultaHistorial Estado,
    IReadOnlyList<CarritoHistorialDto> Carritos);