using MediatR;

namespace NEXORA.Application.Features.Usuario.ListarUsuarios;

public sealed record ListarUsuariosQuery(string Nombre, string Contrasena)
    : IRequest<ListarUsuariosResponse>;