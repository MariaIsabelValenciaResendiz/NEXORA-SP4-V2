using MediatR;

namespace NEXORA.Application.Features.Auth.Login;

public record LoginCommand(string Nombre, string Contrasena) : IRequest<LoginResponse?>;