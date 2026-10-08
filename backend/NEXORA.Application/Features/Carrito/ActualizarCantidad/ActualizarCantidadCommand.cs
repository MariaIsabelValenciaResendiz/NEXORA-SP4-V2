using MediatR;
using NEXORA.Application.Features.Carrito.ConsultarCarrito;

namespace NEXORA.Application.Features.Carrito.ActualizarCantidad;

public sealed record ActualizarCantidadCommand(
    int ClienteId,
    int ProductoId,
    int Cantidad) : IRequest<ArticuloCarritoDto>;
