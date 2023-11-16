using System;
using Assimalign.OGraph.Gdm;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Assimalign.OGraph.AspNetCore;


public static class OGraphServiceCollectionExtensions
{
    private static readonly IOGraphExecutorBuilder builder = new OGraphExecutorBuilder();

    public static IServiceCollection AddOGraph(this IServiceCollection services, Label name, Action<IOGraphGdmBuilder> configure)
    {
        builder.ConfigureModel(name, configure);
        services.TryAddSingleton<IOGraphExecutorBuilder>(builder);
        services.TryAddSingleton<IOGraphExecutor>(serviceProvider =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<OGraphExecutorOptions>>().Value;

            return builder.Build();
        });
        return services;
    }

    public static IServiceCollection AddOGraphOptions(this IServiceCollection services, Action<OGraphExecutorOptions> configure)
    {
        builder.ConfigureOptions(configure);

        return services;
    }
}
