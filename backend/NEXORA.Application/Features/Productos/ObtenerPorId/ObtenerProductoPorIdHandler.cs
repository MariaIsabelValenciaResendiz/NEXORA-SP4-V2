using FluentValidation;
using MediatR;
using NEXORA.Application.Interfaces;

namespace NEXORA.Application.Features.Productos.ObtenerPorId;

public sealed class ObtenerProductoPorIdHandler
    : IRequestHandler<
        ObtenerProductoPorIdQuery,
        ProductoDetalleResponse>
{
    private readonly IProductoRepository _productos;

    public ObtenerProductoPorIdHandler(
        IProductoRepository productos)
    {
        _productos = productos;
    }

    public Task<ProductoDetalleResponse> Handle(
        ObtenerProductoPorIdQuery consulta,
        CancellationToken cancellationToken)
    {
        var producto = _productos.ObtenerPorId(
            consulta.Id)
            ?? throw new ValidationException(
                "El producto solicitado no existe.");

        var respuesta =
            new ProductoDetalleResponse(
                producto.Id,
                producto.Nombre,
                producto.ImagenUrl,
                producto.Descripcion,
                producto.Precio,
                producto.Categoria,
                producto.Stock);

        return Task.FromResult(respuesta);
    }
}
