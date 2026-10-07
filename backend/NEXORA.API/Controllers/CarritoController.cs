using MediatR;
using Microsoft.AspNetCore.Mvc;
using NEXORA.Application.Features.Carrito.AgregarArticulo;

namespace NEXORA.API.Controllers;

[ApiController]
[Route("api/carrito")]
public sealed class CarritoController : ControllerBase
{
    private readonly IMediator _mediator;

    public CarritoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<AgregarArticuloResponse>> Agregar(
        [FromBody] AgregarArticuloCommand comando,
        CancellationToken cancellationToken)
    {
        var respuesta = await _mediator.Send(comando, cancellationToken);
        return Ok(respuesta);
    }
}
