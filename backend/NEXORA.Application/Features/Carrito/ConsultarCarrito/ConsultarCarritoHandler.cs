using MediatR;
using NEXORA.Application.Interfaces;

namespace NEXORA.Application.Features.Carrito.ConsultarCarrito;

public sealed class ConsultarCarritoHandler : IRequestHandler<ConsultarCarritoQuery, IReadOnlyList<ArticuloCarritoDto>>
{
    private readonly ICarritoRepository _carrito;
    private readonly IProductoRepository _productos;

    public ConsultarCarritoHandler(ICarritoRepository carrito, IProductoRepository productos)
    {
        _carrito = carrito;
        _productos = productos;
    }

    public Task<IReadOnlyList<ArticuloCarritoDto>> Handle(
        ConsultarCarritoQuery consulta,
        CancellationToken cancellationToken)
    {
        var resultado = _carrito.ObtenerPorCliente(consulta.ClienteId)
            .Select(articulo => new
            {
                Articulo = articulo,
                Producto = _productos.ObtenerPorId(articulo.ProductoId)
            })
            .Where(item => item.Producto is not null)
            .Select(item =>
            {
                var producto = item.Producto!;
                return new ArticuloCarritoDto(
                    producto.Id,
                    producto.Nombre,
                    producto.Precio,
                    producto.ImagenUrl,
                    item.Articulo.Cantidad,
                    decimal.Round(producto.Precio * item.Articulo.Cantidad, 2));
            })
            .ToArray();

        return Task.FromResult<IReadOnlyList<ArticuloCarritoDto>>(resultado);
    }
}
