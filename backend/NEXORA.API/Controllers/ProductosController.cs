using MediatR;
using Microsoft.AspNetCore.Mvc;
using NEXORA.Application.Features.Producto.FiltrarPorCategoria;
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
        var resultado = await _mediator.Send(
            new ObtenerProductoPorIdQuery(id),
            cancellationToken);

        return Ok(resultado);
    }

    [HttpGet("categorias")]
    public async Task<ActionResult<IReadOnlyList<string>>> ObtenerCategorias(
        CancellationToken cancellationToken)
    {
        var resultado = await _mediator.Send(
            new ObtenerCategoriasQuery(),
            cancellationToken);

        return Ok(resultado);
    }

    [HttpGet("por-categoria")]
    public async Task<ActionResult<IReadOnlyList<ProductoCatalogoDto>>> ObtenerPorCategoria(
        [FromQuery] string? categoria,
        CancellationToken cancellationToken)
    {
        var resultado = await _mediator.Send(
            new ObtenerProductosPorCategoriaQuery(categoria ?? string.Empty),
            cancellationToken);

        return Ok(resultado);
    }
}