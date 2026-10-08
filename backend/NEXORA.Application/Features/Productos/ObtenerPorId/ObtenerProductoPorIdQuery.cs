using MediatR;

namespace NEXORA.Application.Features.Productos.ObtenerPorId;

public sealed record ObtenerProductoPorIdQuery(int Id) : IRequest<ProductoDetalleResponse>;
