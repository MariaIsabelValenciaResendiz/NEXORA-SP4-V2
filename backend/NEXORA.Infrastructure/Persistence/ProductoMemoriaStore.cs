using System.Collections.Concurrent;
using NEXORA.Domain.Entities;

namespace NEXORA.Infrastructure.Persistence;

internal static class ProductoMemoriaStore
{
    private static readonly ConcurrentDictionary<int, Producto> Productos =
        new(
            ProductosDatosCrudos.Todos.ToDictionary(
                producto => producto.Id,
                producto => producto
            )
        );

    private static int _ultimoId =
        ProductosDatosCrudos.Todos.Count == 0
            ? 0
            : ProductosDatosCrudos.Todos.Max(producto => producto.Id);

    public static IReadOnlyList<Producto> ObtenerTodos()
    {
        return Productos.Values
            .OrderBy(producto => producto.Id)
            .ToList();
    }

    public static Producto? ObtenerPorId(int id)
    {
        return Productos.TryGetValue(id, out var producto)
            ? producto
            : null;
    }

    public static IReadOnlyList<string> ObtenerCategorias()
    {
        return Productos.Values
            .Select(producto => producto.Categoria)
            .Where(categoria => !string.IsNullOrWhiteSpace(categoria))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(categoria => categoria)
            .ToList();
    }

    public static IReadOnlyList<Producto> ObtenerPorCategoria(
        string categoria)
    {
        return Productos.Values
            .Where(producto =>
                string.Equals(
                    producto.Categoria,
                    categoria,
                    StringComparison.OrdinalIgnoreCase))
            .OrderBy(producto => producto.Id)
            .ToList();
    }

    public static Producto Agregar(Producto producto)
    {
        var nuevoId = Interlocked.Increment(
            ref _ultimoId
        );

        producto.Id = nuevoId;

        if (!Productos.TryAdd(nuevoId, producto))
        {
            throw new InvalidOperationException(
                "No fue posible registrar el producto."
            );
        }

        return producto;
    }
}