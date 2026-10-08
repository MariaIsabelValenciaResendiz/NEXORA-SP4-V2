using FluentValidation;

namespace NEXORA.Application.Features.Usuario.ListarUsuarios;

public sealed class ListarUsuariosValidator : AbstractValidator<ListarUsuariosQuery>
{
    public ListarUsuariosValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Contrasena).NotEmpty().MaximumLength(200);
    }
}