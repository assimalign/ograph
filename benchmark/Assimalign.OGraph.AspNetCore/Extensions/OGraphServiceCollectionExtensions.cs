using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Assimalign.OGraph.AspNetCore;


public static class OGraphServiceCollectionExtensions
{
    public static IServiceCollection AddOGraph(this IServiceCollection services, Label name, Action<IOGraphModelDescriptor> configure)
    {
        return services
            .AddOGraphOptions(options => { })
            .AddSingleton<IOGraphContext>(serviceProvider =>
            {
                return OGraphBuilder.Create(name, configure);
            })
            .AddSingleton<IOGraphExecutor>(serviceProvider =>
            {
                var graph           = serviceProvider.GetRequiredService<IOGraphContext>();
                var graphOptions    = serviceProvider.GetRequiredService<IOptions<OGraphOptions>>().Value;

                graphOptions.ServiceProvider ??= serviceProvider.GetRequiredService<IServiceProvider>();
                
                return new OGraphExecutor(graph, graphOptions);
            });
    }

    public static IServiceCollection AddOGraphOptions(this IServiceCollection services, Action<OGraphOptions> configure)
    {
        services.AddOptions<OGraphOptions>()
            .Configure(configure);

        return services;
    }
}
