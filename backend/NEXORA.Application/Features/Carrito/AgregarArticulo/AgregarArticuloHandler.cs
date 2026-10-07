using FluentValidation;
using MediatR;
using NEXORA.Application.Interfaces;
using NEXORA.Domain.Entities;

namespace NEXORA.Application.Features.Carrito.AgregarArticulo;

public sealed class AgregarArticuloHandler : IRequestHandler<AgregarArticuloCommand, AgregarArticuloResponse>
{
    private readonly ICarritoRepository _carrito;
    private readonly IProductoRepository _productos;

    public AgregarArticuloHandler(ICarritoRepository carrito, IProductoRepository productos)
    {
        _carrito = carrito;
        _productos = productos;
    }

    public Task<AgregarArticuloResponse> Handle(
        AgregarArticuloCommand comando,
        CancellationToken cancellationToken)
    {
        var producto = _productos.ObtenerPorId(comando.ProductoId)
            ?? throw new ValidationException("El producto solicitado no existe.");

        var articulo = _carrito.AgregarOIncrementar(new ArticuloCarrito
        {
            ClienteId = comando.ClienteId,
            ProductoId = comando.ProductoId,
            Cantidad = comando.Cantidad
        });

        var respuesta = new AgregarArticuloResponse(
            producto.Id,
            producto.Nombre,
            producto.Precio,
            articulo.Cantidad,
            $"Producto añadido. Cantidad: {articulo.Cantidad}");

        return Task.FromResult(respuesta);
    }
}
