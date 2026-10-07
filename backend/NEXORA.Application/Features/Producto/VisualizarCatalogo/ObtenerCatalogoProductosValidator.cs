using FluentValidation;

namespace NEXORA.Application.Features.Producto.VisualizarCatalogo;

public sealed class ObtenerCatalogoProductosValidator
    : AbstractValidator<ObtenerCatalogoProductosQuery>
{
    public ObtenerCatalogoProductosValidator()
    {
    }
}
