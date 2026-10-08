using NEXORA.Application.Interfaces;
using NEXORA.Domain.Entities;
using NEXORA.Infrastructure.Persistence;

namespace NEXORA.Infrastructure.Repositories;

public class ProductoRepositoryMemoria
    : IProductoRepository,
      IProductoEscrituraRepository
{
    public IReadOnlyList<Producto> ObtenerTodos()
    {
        return ProductoMemoriaStore.ObtenerTodos();
    }

    public Producto? ObtenerPorId(int id)
    {
        return ProductoMemoriaStore.ObtenerPorId(id);
    }

    public IReadOnlyList<string> ObtenerCategorias()
    {
        return ProductoMemoriaStore.ObtenerCategorias();
    }

    public IReadOnlyList<Producto> ObtenerPorCategoria(
        string categoria)
    {
        return ProductoMemoriaStore.ObtenerPorCategoria(
            categoria
        );
    }

    public Producto Agregar(Producto producto)
    {
        return ProductoMemoriaStore.Agregar(
            producto
        );
    }
}