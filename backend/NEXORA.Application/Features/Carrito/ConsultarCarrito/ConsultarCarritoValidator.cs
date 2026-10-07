using FluentValidation;

namespace NEXORA.Application.Features.Carrito.ConsultarCarrito;

public sealed class ConsultarCarritoValidator : AbstractValidator<ConsultarCarritoQuery>
{
    public ConsultarCarritoValidator()
    {
        RuleFor(consulta => consulta.ClienteId)
            .GreaterThan(0)
            .WithMessage("El identificador del cliente debe ser mayor que cero.");
    }
}
