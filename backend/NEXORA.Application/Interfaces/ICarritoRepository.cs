using NEXORA.Domain.Entities;

namespace NEXORA.Application.Interfaces;

public interface ICarritoRepository
{
    ArticuloCarrito AgregarOIncrementar(ArticuloCarrito articulo);
    IReadOnlyCollection<ArticuloCarrito> ObtenerPorCliente(int clienteId);
    ArticuloCarrito? ActualizarCantidad(int clienteId, int productoId, int cantidad);
    bool Eliminar(int clienteId, int productoId);
}
