using MediatR;
using Microsoft.AspNetCore.Mvc;
using NEXORA.Application.Features.Producto.VisualizarCatalogo;
using NEXORA.Application.Features.Productos.ObtenerPorId;

namespace NEXORA.API.Controllers;

[ApiController]
[Route("api/productos")]
public sealed class ProductosController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductoCatalogoDto>>> ObtenerTodos(
        CancellationToken cancellationToken)
    {
        var resultado = await _mediator.Send(
            new ObtenerCatalogoProductosQuery(),
            cancellationToken);

        return Ok(resultado);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductoDetalleResponse>> ObtenerPorId(
        int id,
        CancellationToken cancellationToken)
    {
        var respuesta = await _mediator.Send(
            new ObtenerProductoPorIdQuery(id),
            cancellationToken);

        return Ok(respuesta);
    }
}