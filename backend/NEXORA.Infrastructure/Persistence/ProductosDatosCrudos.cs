using NEXORA.Domain.Entities;

namespace NEXORA.Infrastructure.Persistence;

public static class ProductosDatosCrudos
{
    public static readonly IReadOnlyList<Producto> Todos = new List<Producto>
    {
        new()
        {
            Id = 1,
            Nombre = "Audífonos inalámbricos",
            Descripcion = "Audífonos con cancelación de ruido",
            Precio = 499.00m,
            Categoria = "Electrónica",
            Stock = 20,
            ImagenUrl = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e"
        },

        new()
        {
            Id = 2,
            Nombre = "Camisa de algodón",
            Descripcion = "Camisa casual de manga larga",
            Precio = 349.00m,
            Categoria = "Ropa",
            Stock = 35,
            ImagenUrl = "https://images.unsplash.com/photo-1602810318383-e386cc2a3ccf"
        },

        new()
        {
            Id = 3,
            Nombre = "Mochila urbana",
            Descripcion = "Mochila resistente al agua",
            Precio = 599.00m,
            Categoria = "Accesorios",
            Stock = 15,
            ImagenUrl = "https://images.unsplash.com/photo-1553062407-98eeb64c6a62"
        },

        new()
        {
            Id = 4,
            Nombre = "Lámpara de escritorio",
            Descripcion = "Lámpara LED con brillo ajustable",
            Precio = 279.00m,
            Categoria = "Hogar",
            Stock = 40,
            ImagenUrl = "https://images.unsplash.com/photo-1507473885765-e6ed057f782c"
        },

        new()
        {
            Id = 5,
            Nombre = "Reloj clásico",
            Descripcion = "Reloj analógico con correa de piel",
            Precio = 899.00m,
            Categoria = "Joyería",
            Stock = 10,
            ImagenUrl = "https://images.unsplash.com/photo-1523275335684-37898b6baf30"
        }
    };
}
