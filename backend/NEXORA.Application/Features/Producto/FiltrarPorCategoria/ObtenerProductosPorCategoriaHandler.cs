using MediatR;
using NEXORA.Application.Features.Producto.VisualizarCatalogo;
using NEXORA.Application.Interfaces;

namespace NEXORA.Application.Features.Producto.FiltrarPorCategoria;

public sealed class ObtenerProductosPorCategoriaHandler
    : IRequestHandler<
        ObtenerProductosPorCategoriaQuery,
        IReadOnlyList<ProductoCatalogoDto>>
{
    private readonly IProductoRepository _productoRepository;

    public ObtenerProductosPorCategoriaHandler(
        IProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }

    public Task<IReadOnlyList<ProductoCatalogoDto>> Handle(
        ObtenerProductosPorCategoriaQuery request,
        CancellationToken cancellationToken)
    {
        var productos = _productoRepository.ObtenerPorCategoria(
            request.Categoria.Trim());

        IReadOnlyList<ProductoCatalogoDto> resultado = productos
            .Select(producto => new ProductoCatalogoDto(
                producto.Id,
                producto.Nombre,
                producto.ImagenUrl,
                producto.Precio,
                producto.Categoria,
                producto.Descripcion,
                producto.Stock
            ))
            .ToList();

        return Task.FromResult(resultado);
    }
}
