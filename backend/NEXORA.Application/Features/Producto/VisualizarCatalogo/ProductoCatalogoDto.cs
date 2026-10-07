namespace NEXORA.Application.Features.Producto.VisualizarCatalogo;

public sealed record ProductoCatalogoDto(
    int Id,
    string Nombre,
    string ImagenUrl,
    decimal Precio,
    string Categoria,
    string Descripcion
);
