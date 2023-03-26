
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Assimalign.OGraph.AspNetCore;


public static class OGraphServiceCollectionExtensions
{
    public static IServiceCollection AddOGraph(this IServiceCollection services, Name name, Action<IOGraphBuilder> configure)
    {
        
        return services
            .AddOGraphOptions(options => { })
            .AddSingleton<IOGraph>(serviceProvider =>
            {
                return OGraphBuilder.Create(name, configure);
            })
            .AddSingleton<IOGraphExecutor>(serviceProvider =>
            {
                var graph           = serviceProvider.GetRequiredService<IOGraph>();
                var graphOptions    = serviceProvider.GetRequiredService<IOptions<OGraphOptions>>().Value;


                graphOptions.ServiceProvider ??= serviceProvider.GetRequiredService<IServiceProvider>();
                


                return new OGraphExecutor(graph, graphOptions);
            });
    }

    public static IServiceCollection AddOGraphOptions(this IServiceCollection services, Action<OGraphOptions> configure)
    {
        services.AddOptions<OGraphOptions>()
            .Configure(options =>
            {
                configure.Invoke(options);
            });

        return services;
    }
}
