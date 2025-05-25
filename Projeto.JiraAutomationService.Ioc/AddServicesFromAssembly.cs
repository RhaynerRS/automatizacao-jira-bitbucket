using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddServicesFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        var typesWithInterfaces = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .Select(t => new
            {
                Implementation = t,
                Interface = t.GetInterface($"I{t.Name}")
            })
            .Where(t => t.Interface != null);

        foreach (var type in typesWithInterfaces)
        {
            services.AddScoped(type.Interface, type.Implementation);
        }
    }
}
