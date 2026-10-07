using FluentValidation;

namespace NEXORA.Application.Features.Auth.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(c => c.Nombre)
            .NotEmpty().WithMessage("El usuario es obligatorio.");

        RuleFor(c => c.Contrasena)
            .NotEmpty().WithMessage("La contraseña es obligatoria.");
    }
}