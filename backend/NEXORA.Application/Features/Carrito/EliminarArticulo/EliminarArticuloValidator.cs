using FluentValidation;

namespace NEXORA.Application.Features.Carrito.EliminarArticulo;

public sealed class EliminarArticuloValidator : AbstractValidator<EliminarArticuloCommand>
{
    public EliminarArticuloValidator()
    {
        RuleFor(comando => comando.ClienteId)
            .GreaterThan(0)
            .WithMessage("El identificador del cliente debe ser mayor que cero.");

        RuleFor(comando => comando.ProductoId)
            .GreaterThan(0)
            .WithMessage("El identificador del producto debe ser mayor que cero.");
    }
}
