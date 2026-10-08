namespace NEXORA.Application.Features.Usuario.ListarUsuarios;

public sealed record ListarUsuariosResponse(
    EstadoConsultaUsuarios Estado,
    IReadOnlyList<UsuarioDirectorioDto> Usuarios);