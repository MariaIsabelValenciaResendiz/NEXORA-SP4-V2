using MediatR;
using NEXORA.Application.Interfaces;

namespace NEXORA.Application.Features.Producto.FiltrarPorCategoria;

public sealed class ObtenerCategoriasHandler
    : IRequestHandler<ObtenerCategoriasQuery, IReadOnlyList<string>>
{
    private readonly IProductoRepository _productoRepository;

    public ObtenerCategoriasHandler(
        IProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }

    public Task<IReadOnlyList<string>> Handle(
        ObtenerCategoriasQuery request,
        CancellationToken cancellationToken)
    {
        var categorias = _productoRepository.ObtenerCategorias();

        return Task.FromResult(categorias);
    }
}
