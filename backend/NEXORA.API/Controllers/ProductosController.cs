using MediatR;
using Microsoft.AspNetCore.Mvc;
using NEXORA.Application.Features.Producto.VisualizarCatalogo;

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
    public async Task<ActionResult<IReadOnlyList<ProductoCatalogoDto>>> ObtenerTodos()
    {
        var resultado = await _mediator.Send(
            new ObtenerCatalogoProductosQuery());

        return Ok(resultado);
    }
}
