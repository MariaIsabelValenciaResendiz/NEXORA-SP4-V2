using NEXORA.Domain.Entities;

namespace NEXORA.Application.Interfaces;

public interface IProductoEscrituraRepository
{
    Producto Agregar(Producto producto);
}