using MediatR;

namespace NEXORA.Application.Features.Carrito.AgregarArticulo;

public sealed record AgregarArticuloCommand(
    int ClienteId,
    int ProductoId,
    int Cantidad) : IRequest<AgregarArticuloResponse>;
