
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Assimalign.OGraph.AspNetCore;


public static class OGraphServiceCollectionExtensions
{
    public static IServiceCollection AddOGraph(this IServiceCollection service, Name name, Action<IOGraphBuilder> configure)
    {
        return service
            .AddSingleton<IOGraph>(serviceProvider =>
            {
                return OGraphBuilder.Create(name, configure);
            })
            .AddSingleton<IOGraphHttpExecutor>(serviceProvider =>
            {
                var graph = serviceProvider.GetRequiredService<IOGraph>();

                return new OGraphHttpExecutor()
                {
                    
                };
            });
    }
}
