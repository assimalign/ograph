
using System;
using System.Linq;
using System.Threading.Tasks;
using Assimalign.OGraph.AspNetCore.Internal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Assimalign.OGraph.AspNetCore;

public static class OGraphWebApplicationExtensions
{
    public static WebApplication MapOGraphBinding<T>(this WebApplication app, Action<IOGraphApplicationOperationDescriptor<T>> descriptor) where T : class, new()
    {
        var builder = app.Services.GetRequiredService<IOGraphExecutorBuilder>();

        builder.ConfigureApplication(configure =>
        {
            configure.Bind<T>(descriptor);
        });

        return app;
    }

    public static void RunOGraph(this WebApplication app)
    {
        Func<HttpContext, RequestDelegate, Task> task = (context, next) =>
        {
            var serviceProvider = context.RequestServices;
            var executor = serviceProvider.GetRequiredService<IOGraphExecutor>();

            return executor.ExecuteAsync(new ExecutorContext(context), default);
        };
        app.Use(task);
        app.Run();
    }
}
