using NEXORA.Domain.Entities;

namespace NEXORA.Application.Features.Usuario.ListarUsuarios;

public sealed record UsuarioDirectorioDto(
    int Id,
    string Nombre,
    RolUsuario Rol,
    string NombreCompleto,
    string Correo,
    string Telefono,
    DireccionUsuarioDto Direccion);