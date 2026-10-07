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
        return ProductosDatosCrudos.Todos
            .FirstOrDefault(producto => producto.Id == id);
    }

    public IReadOnlyList<string> ObtenerCategorias()
    {
        return ProductosDatosCrudos.Todos
            .Select(producto => producto.Categoria)
            .Where(categoria => !string.IsNullOrWhiteSpace(categoria))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(categoria => categoria)
            .ToList();
    }

    public IReadOnlyList<Producto> ObtenerPorCategoria(string categoria)
    {
        return ProductosDatosCrudos.Todos
            .Where(producto =>
                string.Equals(
                    producto.Categoria,
                    categoria,
                    StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}
