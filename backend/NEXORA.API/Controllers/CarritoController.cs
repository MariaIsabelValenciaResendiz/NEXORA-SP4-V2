using MediatR;
using Microsoft.AspNetCore.Mvc;
using NEXORA.Application.Features.Carrito.ActualizarCantidad;
using NEXORA.Application.Features.Carrito.AgregarArticulo;
using NEXORA.Application.Features.Carrito.ConsultarCarrito;
using NEXORA.Application.Features.Carrito.EliminarArticulo;

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

    [HttpGet("{clienteId:int}")]
    public async Task<ActionResult<IReadOnlyList<ArticuloCarritoDto>>> ObtenerPorCliente(
        int clienteId,
        CancellationToken cancellationToken)
    {
        var resultado = await _mediator.Send(new ConsultarCarritoQuery(clienteId), cancellationToken);
        return Ok(resultado);
    }

    [HttpPut("{clienteId:int}/articulos/{productoId:int}")]
    public async Task<ActionResult<ArticuloCarritoDto>> ActualizarCantidad(
        int clienteId,
        int productoId,
        [FromBody] ActualizarCantidadRequest solicitud,
        CancellationToken cancellationToken)
    {
        var comando = new ActualizarCantidadCommand(clienteId, productoId, solicitud.Cantidad);
        var resultado = await _mediator.Send(comando, cancellationToken);
        return Ok(resultado);
    }

    [HttpDelete("{clienteId:int}/articulos/{productoId:int}")]
    public async Task<IActionResult> Eliminar(
        int clienteId,
        int productoId,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(new EliminarArticuloCommand(clienteId, productoId), cancellationToken);
        return NoContent();
    }
}

public sealed record ActualizarCantidadRequest(int Cantidad);
