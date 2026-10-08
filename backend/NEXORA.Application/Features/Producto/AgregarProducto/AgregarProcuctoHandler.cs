using MediatR;
using NEXORA.Application.Interfaces;
using ProductoEntidad = NEXORA.Domain.Entities.Producto;
namespace NEXORA.Application.Features.Producto.AgregarProducto;

public sealed class AgregarProductoHandler
    : IRequestHandler<
        AgregarProductoCommand,
        ProductoCreadoDto>
{
    private readonly IProductoEscrituraRepository
        _productoRepository;

    public AgregarProductoHandler(
        IProductoEscrituraRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }

    public Task<ProductoCreadoDto> Handle(
        AgregarProductoCommand comando,
        CancellationToken cancellationToken)
    {
        var producto = new ProductoEntidad
        {
            Nombre = comando.Nombre,
            Precio = comando.Precio,
            Categoria = comando.Categoria,
            ImagenUrl = comando.ImagenUrl,
            Descripcion = comando.Descripcion,
            Stock = 0
        };

        var productoCreado =
            _productoRepository.Agregar(producto);

        var respuesta =
            new ProductoCreadoDto(
                productoCreado.Id
            );

        return Task.FromResult(respuesta);
    }
}