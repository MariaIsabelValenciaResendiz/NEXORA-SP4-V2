using MediatR;

namespace NEXORA.Application.Features.Carrito.EliminarArticulo;

public sealed record EliminarArticuloCommand(int ClienteId, int ProductoId) : IRequest<Unit>;
