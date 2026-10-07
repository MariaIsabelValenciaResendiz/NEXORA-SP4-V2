namespace NEXORA.Application.Features.Usuario.ListarUsuarios;

public sealed record DireccionUsuarioDto(
    string Ciudad,
    string Calle,
    int Numero,
    string CodigoPostal,
    CoordenadasUsuarioDto Geolocalizacion);