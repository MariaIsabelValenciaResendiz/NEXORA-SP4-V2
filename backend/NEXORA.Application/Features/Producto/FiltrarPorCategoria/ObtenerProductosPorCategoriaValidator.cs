using FluentValidation;

namespace NEXORA.Application.Features.Producto.FiltrarPorCategoria;

public sealed class ObtenerProductosPorCategoriaValidator
    : AbstractValidator<ObtenerProductosPorCategoriaQuery>
{
    public ObtenerProductosPorCategoriaValidator()
    {
        RuleFor(consulta => consulta.Categoria)
            .Must(categoria => !string.IsNullOrWhiteSpace(categoria))
            .WithMessage("La categoría es obligatoria.");
    }
}
