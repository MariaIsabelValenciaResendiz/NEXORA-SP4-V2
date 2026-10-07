using FluentValidation;
using NEXORA.Application.Interfaces;

namespace NEXORA.Application.Features.Carrito.AgregarArticulo;

public sealed class AgregarArticuloValidator : AbstractValidator<AgregarArticuloCommand>
{
    public AgregarArticuloValidator(IProductoRepository productos)
    {
        RuleFor(comando => comando.ClienteId)
            .GreaterThan(0)
            .WithMessage("El identificador del cliente debe ser mayor que cero.");

        RuleFor(comando => comando.ProductoId)
            .GreaterThan(0)
            .WithMessage("El identificador del producto debe ser mayor que cero.")
            .Must(productoId => productos.ObtenerPorId(productoId) is not null)
            .WithMessage("El producto solicitado no existe.");

        RuleFor(comando => comando.Cantidad)
            .GreaterThan(0)
            .WithMessage("La cantidad debe ser mayor que cero.");
    }
}
