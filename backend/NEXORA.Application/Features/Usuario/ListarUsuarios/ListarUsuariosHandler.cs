using MediatR;
using NEXORA.Application.Interfaces;
using NEXORA.Domain.Entities;
using EntidadUsuario = NEXORA.Domain.Entities.Usuario;

namespace NEXORA.Application.Features.Usuario.ListarUsuarios;

public sealed class ListarUsuariosHandler(IUsuarioRepository repositorio)
    : IRequestHandler<ListarUsuariosQuery, ListarUsuariosResponse>
{
    public Task<ListarUsuariosResponse> Handle(
        ListarUsuariosQuery consulta,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var usuario = repositorio.ObtenerPorCredenciales(
            consulta.Nombre,
            consulta.Contrasena);

        if (usuario is null)
        {
            return Task.FromResult(
                new ListarUsuariosResponse(
                    EstadoConsultaUsuarios.SinAutenticar, []));
        }

        if (usuario.Rol is not
            (RolUsuario.Administrador or RolUsuario.Auditor))
        {
            return Task.FromResult(
                new ListarUsuariosResponse(
                    EstadoConsultaUsuarios.SinPermiso, []));
        }

        var usuarios = repositorio.ObtenerTodos()
            .Select(Mapear)
            .ToArray();

        return Task.FromResult(
            new ListarUsuariosResponse(
                EstadoConsultaUsuarios.Correcto, usuarios));
    }

    private static UsuarioDirectorioDto Mapear(EntidadUsuario usuario)
    {
        var direccion = usuario.Direccion ?? new DireccionUsuario();
        var coordenadas = direccion.Geolocalizacion ?? new CoordenadasUsuario();

        return new UsuarioDirectorioDto(
            usuario.Id,
            usuario.Nombre ?? string.Empty,
            usuario.Rol,
            usuario.NombreCompleto ?? string.Empty,
            usuario.Correo ?? string.Empty,
            usuario.Telefono ?? string.Empty,
            new DireccionUsuarioDto(
                direccion.Ciudad ?? string.Empty,
                direccion.Calle ?? string.Empty,
                direccion.Numero,
                direccion.CodigoPostal ?? string.Empty,
                new CoordenadasUsuarioDto(
                    coordenadas.Latitud ?? string.Empty,
                    coordenadas.Longitud ?? string.Empty)));
    }
}