using FluentValidation;
using NEXORA.Application.Interfaces;

namespace NEXORA.Application.Features.Productos.ObtenerPorId;

public sealed class ObtenerProductoPorIdValidator : AbstractValidator<ObtenerProductoPorIdQuery>
{
    public ObtenerProductoPorIdValidator(IProductoRepository productos)
    {
        RuleFor(consulta => consulta.Id)
            .GreaterThan(0)
            .WithMessage("El identificador del producto debe ser mayor que cero.")
            .Must(id => productos.ObtenerPorId(id) is not null)
            .WithMessage("El producto solicitado no existe.");
    }
}
