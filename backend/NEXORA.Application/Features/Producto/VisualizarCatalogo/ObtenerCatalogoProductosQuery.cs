using MediatR;

namespace NEXORA.Application.Features.Producto.VisualizarCatalogo;

public sealed record ObtenerCatalogoProductosQuery
    : IRequest<IReadOnlyList<ProductoCatalogoDto>>;
    