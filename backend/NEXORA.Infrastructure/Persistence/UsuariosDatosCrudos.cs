using NEXORA.Domain.Entities;

namespace NEXORA.Infrastructure.Persistence;

public static class UsuariosDatosCrudos
{
    public static readonly IReadOnlyList<Usuario> Todos = new List<Usuario>
    {
        new() { Id = 1, Nombre = "admin", Contrasena = "admin123", Rol = RolUsuario.Administrador },
        new() { Id = 2, Nombre = "cliente", Contrasena = "cliente123", Rol = RolUsuario.Cliente },
        new() { Id = 3, Nombre = "auditor", Contrasena = "auditor123", Rol = RolUsuario.Auditor }
    };
}