using FluentValidation;

namespace NEXORA.Application.Features.Producto.FiltrarPorCategoria;

public sealed class ObtenerCategoriasValidator
    : AbstractValidator<ObtenerCategoriasQuery>
{
    public ObtenerCategoriasValidator()
    {
    }
}
