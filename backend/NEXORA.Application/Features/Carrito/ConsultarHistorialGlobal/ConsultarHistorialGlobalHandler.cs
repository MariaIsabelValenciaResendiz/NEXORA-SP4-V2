using MediatR;
using NEXORA.Application.Interfaces;
using NEXORA.Domain.Entities;

namespace NEXORA.Application.Features.Carrito.ConsultarHistorialGlobal;

public sealed class ConsultarHistorialGlobalHandler
    : IRequestHandler<ConsultarHistorialGlobalQuery, ConsultarHistorialGlobalResponse>
{
    private readonly IUsuarioRepository _usuarios;
    private readonly ICarritoRepository _carritos;
    private readonly IProductoRepository _productos;

    public ConsultarHistorialGlobalHandler(
        IUsuarioRepository usuarios,
        ICarritoRepository carritos,
        IProductoRepository productos)
    {
        _usuarios = usuarios;
        _carritos = carritos;
        _productos = productos;
    }

    public Task<ConsultarHistorialGlobalResponse> Handle(
        ConsultarHistorialGlobalQuery consulta,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var usuario = _usuarios.ObtenerPorCredenciales(consulta.Nombre, consulta.Contrasena);

        if (usuario is null)
        {
            return Task.FromResult(new ConsultarHistorialGlobalResponse(
                EstadoConsultaHistorial.SinAutenticar, []));
        }

        if (usuario.Rol is not (RolUsuario.Administrador or RolUsuario.Auditor))
        {
            return Task.FromResult(new ConsultarHistorialGlobalResponse(
                EstadoConsultaHistorial.SinPermiso, []));
        }

        var carritos = _carritos.ObtenerHistorialGlobal()
            .Select(carrito => new CarritoHistorialDto(
                carrito.Id,
                carrito.FechaCreacion,
                carrito.ClienteId,
                carrito.Articulos.Select(articulo => new ProductoCarritoHistorialDto(
                    articulo.ProductoId,
                    articulo.Cantidad,
                    _productos.ObtenerPorId(articulo.ProductoId)?.Nombre
                        ?? $"Producto {articulo.ProductoId}"))
                .ToArray()))
            .ToArray();

        return Task.FromResult(new ConsultarHistorialGlobalResponse(
            EstadoConsultaHistorial.Correcto, carritos));
    }
}