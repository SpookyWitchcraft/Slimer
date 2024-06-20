using Microsoft.Extensions.DependencyInjection.Extensions;
using Slimer.Interfaces;
using System.Reflection;

namespace Slimer.Extensions
{
    public static class EndpointExtensions
    {
        public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
        {
            var serviceDescriptors = assembly
                .DefinedTypes
                .Where(type => type is { IsAbstract: false, IsInterface: false } && type.IsAssignableTo(typeof(IEndpoint)))
                .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
                .ToArray();

            services.TryAddEnumerable(serviceDescriptors);

            return services;
        }

        public static IApplicationBuilder MapEndpoints(this WebApplication app, RouteGroupBuilder? routeGroupBuilder = null)
        {
            var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

            var builder = (IEndpointRouteBuilder?)routeGroupBuilder ?? app;

            foreach (IEndpoint endpoint in endpoints)
                endpoint.MapEndpoint(builder);

            return app;
        }
    }
}
