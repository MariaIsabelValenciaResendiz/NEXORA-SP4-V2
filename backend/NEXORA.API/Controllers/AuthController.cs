using MediatR;
using Microsoft.AspNetCore.Mvc;
using NEXORA.Application.Features.Auth.Login;

namespace NEXORA.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediador;

    public AuthController(IMediator mediador)
    {
        _mediador = mediador;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand comando)
    {
        var respuesta = await _mediador.Send(comando);

        return respuesta is null
            ? Unauthorized(new { mensaje = "Usuario o contraseña incorrectos." })
            : Ok(respuesta);
    }
}