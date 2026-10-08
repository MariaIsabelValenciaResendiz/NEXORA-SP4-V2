using MediatR;

namespace NEXORA.Application.Features.Producto.AgregarProducto;

public sealed record AgregarProductoCommand(
    string Nombre,
    decimal Precio,
    string Categoria,
    string ImagenUrl,
    string Descripcion
) : IRequest<ProductoCreadoDto>;