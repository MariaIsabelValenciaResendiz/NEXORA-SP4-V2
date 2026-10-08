using MediatR;
using Microsoft.AspNetCore.Mvc;
using NEXORA.Application.Features.Producto.FiltrarPorCategoria;
using NEXORA.Application.Features.Producto.VisualizarCatalogo;
using NEXORA.Application.Features.Productos.ObtenerPorId;
using NEXORA.Application.Features.Producto.AgregarProducto;
using NEXORA.API.Contracts.Productos;

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
    [HttpPost]
    public async Task<ActionResult<ProductoCreadoDto>> Agregar(
        [FromBody] AgregarProductoRequest request,
        CancellationToken cancellationToken)
    {
        var comando = new AgregarProductoCommand(
            request.Titulo,
            request.Precio,
            request.Categoria,
            request.ImagenUrl,
            request.Descripcion
        );

        var resultado = await _mediator.Send(
            comando,
            cancellationToken
        );

        return CreatedAtAction(
            nameof(ObtenerPorId),
            new { id = resultado.Id },
            resultado
        );
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