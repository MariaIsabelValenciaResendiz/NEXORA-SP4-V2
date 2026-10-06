using Microsoft.Extensions.DependencyInjection;

namespace NEXORA.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection servicios)
    {
        var ensamblado = typeof(DependencyInjection).Assembly;

        var implementaciones = ensamblado.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract);

        foreach (var implementacion in implementaciones)
        {
            var contratos = implementacion.GetInterfaces()
                .Where(i => i.Name.EndsWith("Repository"));

            foreach (var contrato in contratos)
            {
                servicios.AddScoped(contrato, implementacion);
            }
        }

        return servicios;
    }
}