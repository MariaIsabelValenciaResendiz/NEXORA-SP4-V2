using FluentValidation;

namespace NEXORA.Application.Features.Carrito.ConsultarHistorialGlobal;

public sealed class ConsultarHistorialGlobalValidator
    : AbstractValidator<ConsultarHistorialGlobalQuery>
{
    public ConsultarHistorialGlobalValidator()
    {
        RuleFor(consulta => consulta.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(100).WithMessage("El nombre admite hasta 100 caracteres.");

        RuleFor(consulta => consulta.Contrasena)
            .NotEmpty().WithMessage("La contraseña es obligatoria.")
            .MaximumLength(200).WithMessage("La contraseña admite hasta 200 caracteres.");
    }
}