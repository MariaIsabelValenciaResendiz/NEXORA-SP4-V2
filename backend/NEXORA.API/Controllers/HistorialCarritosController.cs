using MediatR;
using Microsoft.AspNetCore.Mvc;
using NEXORA.API.Contracts.Historial;
using NEXORA.Application.Features.Carrito.ConsultarHistorialGlobal;

namespace NEXORA.API.Controllers;

[ApiController]
[Route("api/carts")]
public sealed class HistorialCarritosController : ControllerBase
{
    private readonly IMediator _mediator;

    public HistorialCarritosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [HttpGet("/carts")]
    public async Task<IActionResult> Obtener(
        [FromHeader(Name = "Authorization")] string? autorizacion,
        CancellationToken cancellationToken)
    {
        var consulta = AutorizacionBasica.Leer(autorizacion);

        if (consulta is null)
        {
            return Unauthorized(new { mensaje = "Debes confirmar tus credenciales." });
        }

        var resultado = await _mediator.Send(consulta, cancellationToken);

        return resultado.Estado switch
        {
            EstadoConsultaHistorial.Correcto => Ok(resultado.Carritos),
            EstadoConsultaHistorial.SinAutenticar => Unauthorized(
                new { mensaje = "Usuario o contraseña incorrectos." }),
            EstadoConsultaHistorial.SinPermiso => StatusCode(StatusCodes.Status403Forbidden,
                new { mensaje = "Solo Administrador y Auditor pueden consultar el historial." }),
            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
    }
}