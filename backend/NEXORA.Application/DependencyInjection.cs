using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NEXORA.Application.Common.Behaviors;

namespace NEXORA.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection servicios)
    {
        var ensamblado = typeof(DependencyInjection).Assembly;

        servicios.AddMediatR(config => config.RegisterServicesFromAssembly(ensamblado));
        servicios.AddValidatorsFromAssembly(ensamblado);
        servicios.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return servicios;
    }
}