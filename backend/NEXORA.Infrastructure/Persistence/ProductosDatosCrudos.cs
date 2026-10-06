using NEXORA.Domain.Entities;

namespace NEXORA.Infrastructure.Persistence;

public static class ProductosDatosCrudos
{
    public static readonly IReadOnlyList<Producto> Todos = new List<Producto>
    {
        new() { Id = 1, Nombre = "Audífonos inalámbricos", Descripcion = "Audífonos con cancelación de ruido", Precio = 499.00m, Categoria = "Electrónica", Stock = 20 },
        new() { Id = 2, Nombre = "Camisa de algodón", Descripcion = "Camisa casual de manga larga", Precio = 349.00m, Categoria = "Ropa", Stock = 35 },
        new() { Id = 3, Nombre = "Mochila urbana", Descripcion = "Mochila resistente al agua", Precio = 599.00m, Categoria = "Accesorios", Stock = 15 },
        new() { Id = 4, Nombre = "Lámpara de escritorio", Descripcion = "Lámpara LED con brillo ajustable", Precio = 279.00m, Categoria = "Hogar", Stock = 40 },
        new() { Id = 5, Nombre = "Reloj clásico", Descripcion = "Reloj analógico con correa de piel", Precio = 899.00m, Categoria = "Joyería", Stock = 10 }
    };
}