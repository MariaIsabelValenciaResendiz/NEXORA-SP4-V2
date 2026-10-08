namespace NEXORA.API.Contracts.Productos;

public sealed record AgregarProductoRequest(
    string Titulo,
    decimal Precio,
    string Categoria,
    string ImagenUrl,
    string Descripcion
);