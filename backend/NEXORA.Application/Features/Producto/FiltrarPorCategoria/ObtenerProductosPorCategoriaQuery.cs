using MediatR;
using NEXORA.Application.Features.Producto.VisualizarCatalogo;

namespace NEXORA.Application.Features.Producto.FiltrarPorCategoria;

public sealed record ObtenerProductosPorCategoriaQuery(
    string Categoria
) : IRequest<IReadOnlyList<ProductoCatalogoDto>>;
