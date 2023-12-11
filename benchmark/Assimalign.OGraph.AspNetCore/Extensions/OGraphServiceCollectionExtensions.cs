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
        services.TryAddSingleton<IOGraphExecutorBuilder>(serviceProvider =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<OGraphExecutorOptions>>();

            return builder.ConfigureOptions(options.Value);
        });
        services.TryAddSingleton<IOGraphExecutor>(serviceProvider =>
        {
            

            return builder.Build();
        });
        return services;
    }

    public static IServiceCollection AddOGraphOptions(this IServiceCollection services, Action<OGraphExecutorOptions> configure)
    {
        services.AddOptions<OGraphExecutorOptions>()
            .Configure(configure);

        return services;
    }
}
