using FluentValidation;
using MediatR;
using NEXORA.Application.Features.Carrito.ConsultarCarrito;
using NEXORA.Application.Interfaces;

namespace NEXORA.Application.Features.Carrito.ActualizarCantidad;

public sealed class ActualizarCantidadHandler : IRequestHandler<ActualizarCantidadCommand, ArticuloCarritoDto>
{
    private readonly ICarritoRepository _carrito;
    private readonly IProductoRepository _productos;

    public ActualizarCantidadHandler(ICarritoRepository carrito, IProductoRepository productos)
    {
        _carrito = carrito;
        _productos = productos;
    }

    public Task<ArticuloCarritoDto> Handle(
        ActualizarCantidadCommand comando,
        CancellationToken cancellationToken)
    {
        var articulo = _carrito.ActualizarCantidad(comando.ClienteId, comando.ProductoId, comando.Cantidad)
            ?? throw new ValidationException("El artículo no existe en el carrito del cliente.");

        var producto = _productos.ObtenerPorId(comando.ProductoId)
            ?? throw new ValidationException("El producto solicitado no existe.");

        var respuesta = new ArticuloCarritoDto(
            producto.Id,
            producto.Nombre,
            producto.Precio,
            producto.ImagenUrl,
            articulo.Cantidad,
            decimal.Round(producto.Precio * articulo.Cantidad, 2));

        return Task.FromResult(respuesta);
    }
}
