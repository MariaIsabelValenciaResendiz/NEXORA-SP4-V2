using NEXORA.Domain.Entities;

namespace NEXORA.Application.Features.Auth.Login;

public record LoginResponse(
    int Id,
    string Nombre,
    RolUsuario Rol,
    string NombreCompleto,
    string Correo);