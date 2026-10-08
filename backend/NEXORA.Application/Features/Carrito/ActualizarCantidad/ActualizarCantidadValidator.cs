using FluentValidation;

namespace NEXORA.Application.Features.Carrito.ActualizarCantidad;

public sealed class ActualizarCantidadValidator : AbstractValidator<ActualizarCantidadCommand>
{
    public ActualizarCantidadValidator()
    {
        RuleFor(comando => comando.ClienteId)
            .GreaterThan(0)
            .WithMessage("El identificador del cliente debe ser mayor que cero.");

        RuleFor(comando => comando.ProductoId)
            .GreaterThan(0)
            .WithMessage("El identificador del producto debe ser mayor que cero.");

        RuleFor(comando => comando.Cantidad)
            .GreaterThan(0)
            .WithMessage("La cantidad debe ser mayor que cero.");
    }
}
