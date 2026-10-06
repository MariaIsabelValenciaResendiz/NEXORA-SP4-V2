using FluentValidation;

namespace NEXORA.API.Middleware;

public class ManejoErroresMiddleware
{
    private readonly RequestDelegate _siguiente;

    public ManejoErroresMiddleware(RequestDelegate siguiente)
    {
        _siguiente = siguiente;
    }

    public async Task InvokeAsync(HttpContext contexto)
    {
        try
        {
            await _siguiente(contexto);
        }
        catch (ValidationException ex)
        {
            contexto.Response.StatusCode = StatusCodes.Status400BadRequest;

            var errores = ex.Errors.Select(e => new
            {
                campo = e.PropertyName,
                mensaje = e.ErrorMessage
            });

            await contexto.Response.WriteAsJsonAsync(new { errores });
        }
    }
}