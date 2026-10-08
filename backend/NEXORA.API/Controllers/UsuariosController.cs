using MediatR;
using Microsoft.AspNetCore.Mvc;
using NEXORA.API.Contracts;
using NEXORA.Application.Features.Usuario.ListarUsuarios;

namespace NEXORA.API.Controllers;

[ApiController]
[Route("api/users")]
[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
public sealed class UsuariosController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [HttpGet("/users")]
    public async Task<IActionResult> Obtener(
        [FromHeader(Name = "Authorization")] string? autorizacion,
        CancellationToken cancellationToken)
    {
        var credenciales = AutorizacionBasica.Leer(autorizacion);

        if (credenciales is null)
        {
            return Unauthorized(
                new { mensaje = "Debes confirmar tus credenciales." });
        }

        var (nombre, contrasena) = credenciales.Value;

        var resultado = await mediator.Send(
            new ListarUsuariosQuery(nombre, contrasena),
            cancellationToken);

        return resultado.Estado switch
        {
            EstadoConsultaUsuarios.Correcto =>
                Ok(resultado.Usuarios),

            EstadoConsultaUsuarios.SinAutenticar =>
                Unauthorized(
                    new { mensaje = "Usuario o contraseña incorrectos." }),

            EstadoConsultaUsuarios.SinPermiso =>
                StatusCode(403,
                    new
                    {
                        mensaje = "Solo Administrador y Auditor pueden consultar usuarios."
                    }),

            _ => StatusCode(500)
        };
    }
}