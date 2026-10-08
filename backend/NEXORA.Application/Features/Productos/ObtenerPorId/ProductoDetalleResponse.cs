namespace NEXORA.Application.Features.Productos.ObtenerPorId;

public sealed record ProductoDetalleResponse(
    int Id,
    string Nombre,
    string Descripcion,
    decimal Precio,
    string Categoria,
    int Stock);
