using NEXORA.Application.Interfaces;
using NEXORA.Domain.Entities;
using NEXORA.Infrastructure.Persistence;

namespace NEXORA.Infrastructure.Repositories;

public sealed class CarritoRepositoryMemoria : ICarritoRepository
{
    public ArticuloCarrito AgregarOIncrementar(ArticuloCarrito articulo)
    {
        lock (CarritoDatosCrudos.Bloqueo)
        {
            var clave = (articulo.ClienteId, articulo.ProductoId);

            if (CarritoDatosCrudos.Articulos.TryGetValue(clave, out var existente))
            {
                existente.Cantidad += articulo.Cantidad;
                return Copiar(existente);
            }

            var nuevo = Copiar(articulo);
            CarritoDatosCrudos.Articulos.Add(clave, nuevo);
            return Copiar(nuevo);
        }
    }

    public IReadOnlyCollection<ArticuloCarrito> ObtenerPorCliente(int clienteId)
    {
        lock (CarritoDatosCrudos.Bloqueo)
        {
            return CarritoDatosCrudos.Articulos.Values
                .Where(articulo => articulo.ClienteId == clienteId)
                .Select(Copiar)
                .ToArray();
        }
    }

    public ArticuloCarrito? ActualizarCantidad(int clienteId, int productoId, int cantidad)
    {
        lock (CarritoDatosCrudos.Bloqueo)
        {
            if (!CarritoDatosCrudos.Articulos.TryGetValue((clienteId, productoId), out var articulo))
            {
                return null;
            }

            articulo.Cantidad = cantidad;
            return Copiar(articulo);
        }
    }

    public bool Eliminar(int clienteId, int productoId)
    {
        lock (CarritoDatosCrudos.Bloqueo)
        {
            return CarritoDatosCrudos.Articulos.Remove((clienteId, productoId));
        }
    }

    private static ArticuloCarrito Copiar(ArticuloCarrito articulo)
    {
        return new ArticuloCarrito
        {
            ClienteId = articulo.ClienteId,
            ProductoId = articulo.ProductoId,
            Cantidad = articulo.Cantidad
        };
    }
}
