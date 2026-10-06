using NEXORA.Domain.Entities;

namespace NEXORA.Application.Interfaces;

public interface IProductoRepository
{
    IReadOnlyList<Producto> ObtenerTodos();
    Producto? ObtenerPorId(int id);
}