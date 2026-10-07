using NEXORA.Domain.Entities;

namespace NEXORA.Application.Interfaces;

public interface ICarritoRepository
{
    ArticuloCarrito AgregarOIncrementar(ArticuloCarrito articulo);
}
