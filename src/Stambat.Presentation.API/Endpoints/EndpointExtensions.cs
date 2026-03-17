using System.Reflection;

using Stambat.WebAPI.Interfaces;

namespace Stambat.WebAPI.Endpoints;

public static class EndpointExtensions
{
    public static void MapEndpointModules(this IEndpointRouteBuilder app)
    {
        IEnumerable<Type> modules = typeof(IEndpointModule).Assembly
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && typeof(IEndpointModule).IsAssignableFrom(t));

        foreach (Type moduleType in modules)
        {
            IEndpointModule? module = (IEndpointModule)Activator.CreateInstance(moduleType)!;
            module.MapEndpoints(app);
        }
    }
}
