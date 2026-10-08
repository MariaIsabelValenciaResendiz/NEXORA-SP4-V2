using MediatR;

namespace NEXORA.Application.Features.Carrito.ConsultarHistorialGlobal;

public sealed record ConsultarHistorialGlobalQuery(
    string Nombre,
    string Contrasena) : IRequest<ConsultarHistorialGlobalResponse>;