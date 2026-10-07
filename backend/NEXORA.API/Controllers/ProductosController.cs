using MediatR;
using Microsoft.AspNetCore.Mvc;
using NEXORA.Application.Features.Producto.FiltrarPorCategoria;
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

    [HttpGet("categorias")]
    public async Task<ActionResult<IReadOnlyList<string>>> ObtenerCategorias()
    {
        var resultado = await _mediator.Send(
            new ObtenerCategoriasQuery());

        return Ok(resultado);
    }

    [HttpGet("por-categoria")]
    public async Task<ActionResult<IReadOnlyList<ProductoCatalogoDto>>> ObtenerPorCategoria(
        [FromQuery] string? categoria)
    {
        var resultado = await _mediator.Send(
            new ObtenerProductosPorCategoriaQuery(
                categoria ?? string.Empty));

        return Ok(resultado);
    }
}
