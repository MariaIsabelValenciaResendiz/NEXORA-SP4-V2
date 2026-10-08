using MediatR;
using NEXORA.Application.Interfaces;

namespace NEXORA.Application.Features.Auth.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse?>
{
    private readonly IUsuarioRepository _usuarios;

    public LoginCommandHandler(IUsuarioRepository usuarios)
    {
        _usuarios = usuarios;
    }

    public Task<LoginResponse?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var usuario = _usuarios.ObtenerPorCredenciales(request.Nombre, request.Contrasena);

        LoginResponse? respuesta = usuario is null
            ? null
            : new LoginResponse(usuario.Id, usuario.Nombre, usuario.Rol);

        return Task.FromResult(respuesta);
    }
}