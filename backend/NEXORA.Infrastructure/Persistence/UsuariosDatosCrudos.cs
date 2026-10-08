using NEXORA.Domain.Entities;

namespace NEXORA.Infrastructure.Persistence;

public static class UsuariosDatosCrudos
{
    public static readonly IReadOnlyList<Usuario> Todos = new List<Usuario>
    {
        new()
        {
            Id = 1,
            Nombre = "admin",
            Contrasena = "admin123",
            Rol = RolUsuario.Administrador,
            NombreCompleto = "Ana López",
            Correo = "admin@nexora.local",
            Telefono = "5550000001",
            Direccion = new()
            {
                Ciudad = "San Juan del Río",
                Calle = "Calle Uno",
                Numero = 10,
                CodigoPostal = "76800",
                Geolocalizacion = new()
                {
                    Latitud = "20.3889",
                    Longitud = "-99.9964"
                }
            }
        },
        new()
        {
            Id = 2,
            Nombre = "cliente",
            Contrasena = "cliente123",
            Rol = RolUsuario.Cliente,
            NombreCompleto = "Carlos Pérez",
            Correo = "cliente@nexora.local",
            Telefono = "5550000002",
            Direccion = new()
            {
                Ciudad = "Querétaro",
                Calle = "Calle Dos",
                Numero = 20,
                CodigoPostal = "76000",
                Geolocalizacion = new()
                {
                    Latitud = "20.5888",
                    Longitud = "-100.3899"
                }
            }
        },
        new()
        {
            Id = 3,
            Nombre = "auditor",
            Contrasena = "auditor123",
            Rol = RolUsuario.Auditor,
            NombreCompleto = "Elena Ruiz",
            Correo = "auditor@nexora.local",
            Telefono = "5550000003",
            Direccion = new()
            {
                Ciudad = "San Juan del Río",
                Calle = "Calle Tres",
                Numero = 30,
                CodigoPostal = "76800",
                Geolocalizacion = new()
                {
                    Latitud = "20.3900",
                    Longitud = "-99.9900"
                }
            }
        }
    };
}