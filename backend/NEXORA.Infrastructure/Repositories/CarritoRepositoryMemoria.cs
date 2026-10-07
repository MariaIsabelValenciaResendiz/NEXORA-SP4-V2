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
