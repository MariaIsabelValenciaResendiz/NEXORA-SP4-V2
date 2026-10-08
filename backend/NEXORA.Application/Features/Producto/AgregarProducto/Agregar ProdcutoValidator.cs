using FluentValidation;

namespace NEXORA.Application.Features.Producto.AgregarProducto;

public sealed class AgregarProductoValidator
    : AbstractValidator<AgregarProductoCommand>
{
    public AgregarProductoValidator()
    {
        RuleFor(comando => comando.Nombre)
            .NotEmpty()
            .WithMessage("El título es obligatorio.");

        RuleFor(comando => comando.Precio)
            .GreaterThan(0)
            .WithMessage("El precio debe ser mayor a cero.");

        RuleFor(comando => comando.Categoria)
            .NotEmpty()
            .WithMessage("La categoría es obligatoria.");

        RuleFor(comando => comando.ImagenUrl)
            .NotEmpty()
            .WithMessage("La URL de imagen es obligatoria.")
            .Must(EsUrlValida)
            .WithMessage("La URL de imagen no es válida.");

        RuleFor(comando => comando.Descripcion)
            .NotEmpty()
            .WithMessage("La descripción es obligatoria.");
    }

    private static bool EsUrlValida(string url)
    {
        return Uri.TryCreate(
            url,
            UriKind.Absolute,
            out var resultado
        )
        && (
            resultado.Scheme == Uri.UriSchemeHttp
            || resultado.Scheme == Uri.UriSchemeHttps
        );
    }
}