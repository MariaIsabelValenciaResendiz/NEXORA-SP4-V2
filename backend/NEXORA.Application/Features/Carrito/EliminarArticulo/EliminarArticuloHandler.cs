using FluentValidation;
using MediatR;
using NEXORA.Application.Interfaces;

namespace NEXORA.Application.Features.Carrito.EliminarArticulo;

public sealed class EliminarArticuloHandler : IRequestHandler<EliminarArticuloCommand, Unit>
{
    private readonly ICarritoRepository _carrito;

    public EliminarArticuloHandler(ICarritoRepository carrito)
    {
        _carrito = carrito;
    }

    public Task<Unit> Handle(EliminarArticuloCommand comando, CancellationToken cancellationToken)
    {
        if (!_carrito.Eliminar(comando.ClienteId, comando.ProductoId))
        {
            throw new ValidationException("El artículo no existe en el carrito del cliente.");
        }

        return Task.FromResult(Unit.Value);
    }
}
