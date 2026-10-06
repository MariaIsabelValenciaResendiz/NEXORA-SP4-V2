using FluentValidation;
using MediatR;

namespace NEXORA.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validadores;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validadores)
    {
        _validadores = validadores;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var contexto = new ValidationContext<TRequest>(request);

        var resultados = await Task.WhenAll(
            _validadores.Select(v => v.ValidateAsync(contexto, cancellationToken)));

        var errores = resultados.SelectMany(r => r.Errors).ToList();

        if (errores.Count != 0)
        {
            throw new ValidationException(errores);
        }

        return await next();
    }
}