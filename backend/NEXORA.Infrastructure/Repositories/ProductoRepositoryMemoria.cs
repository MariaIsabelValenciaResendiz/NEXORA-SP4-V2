using NEXORA.Application.Interfaces;
using NEXORA.Domain.Entities;
using NEXORA.Infrastructure.Persistence;

namespace NEXORA.Infrastructure.Repositories;

public class ProductoRepositoryMemoria : IProductoRepository
{
    public IReadOnlyList<Producto> ObtenerTodos()
    {
        return ProductosDatosCrudos.Todos;
    }

    public Producto? ObtenerPorId(int id)
    {
        return ProductosDatosCrudos.Todos.FirstOrDefault(p => p.Id == id);
    }
}