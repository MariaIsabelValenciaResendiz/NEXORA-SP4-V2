using MediatR;

namespace NEXORA.Application.Features.Producto.FiltrarPorCategoria;

public sealed record ObtenerCategoriasQuery
    : IRequest<IReadOnlyList<string>>;
