namespace NEXORA.Domain.Entities;

public sealed class Carrito
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public DateTimeOffset FechaCreacion { get; set; }
    public IReadOnlyList<ArticuloCarrito> Articulos { get; set; } = [];
}