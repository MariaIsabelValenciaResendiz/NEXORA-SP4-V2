using MediatR;

namespace NEXORA.Application.Features.Carrito.ConsultarCarrito;

public sealed record ConsultarCarritoQuery(int ClienteId) : IRequest<IReadOnlyList<ArticuloCarritoDto>>;
