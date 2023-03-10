using Microsoft.Extensions.DependencyInjection;

namespace Assimalign.OGraph.AspNetCore;

using Assimalign.OGraph;
using Assimalign.OGraph.Execution;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddOGraph(this IServiceCollection service, Name name , Action<IOGraphBuilder> configure)
    {
        return service
            .AddSingleton<IOGraph>(serviceProvider =>
            {
                return OGraphBuilder.Create(name, configure);
            })
            .AddSingleton<IOGraphExecutor>(serviceProvider =>
            {
                var graph = serviceProvider.GetRequiredService<IOGraph>();

                return OGraphExecutor.Create(graph);
            });
    }
}
