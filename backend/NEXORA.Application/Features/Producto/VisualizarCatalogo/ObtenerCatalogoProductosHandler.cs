using MediatR;
using NEXORA.Application.Interfaces;

namespace NEXORA.Application.Features.Producto.VisualizarCatalogo;

public sealed class ObtenerCatalogoProductosHandler
    : IRequestHandler<
        ObtenerCatalogoProductosQuery,
        IReadOnlyList<ProductoCatalogoDto>>
{
    private readonly IProductoRepository _productoRepository;

    public ObtenerCatalogoProductosHandler(
        IProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }

    public Task<IReadOnlyList<ProductoCatalogoDto>> Handle(
        ObtenerCatalogoProductosQuery request,
        CancellationToken cancellationToken)
    {
        var productos = _productoRepository.ObtenerTodos();

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
