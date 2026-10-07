using NEXORA.Domain.Entities;

namespace NEXORA.Infrastructure.Persistence;

public static class CarritoDatosCrudos
{
    internal static readonly object Bloqueo = new();

    public static readonly Dictionary<(int ClienteId, int ProductoId), ArticuloCarrito> Articulos = new();
}
