using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

using Gdm;

internal class ApplicationBuilder : IOGraphApplicationBuilder
{
    private readonly IEnumerable<IOGraphGdm> models;
    public ApplicationBuilder(IEnumerable<IOGraphGdm> models)
    {
        this.models = models;
    }

    public OGraphExecutorOptions Options { get; init; }

    public IOGraphApplicationBuilder Bind<T>(Action<IOGraphApplicationOperationDescriptor<T>> configure) where T : class, new()
    {
        if (configure is null)
        {
            ThrowHelper.ThrowArgumentNullException(nameof(configure));
        }
        foreach (var model in models)
        {
            foreach (var vertex in model.GetGdmVertices())
            {
                if (vertex.IsRuntimeTypeMatch(typeof(T)))
                {
                    var descriptor = new ApplicationOperationDescriptor<T>(vertex)
                    {
                        Options = this.Options
                    };

                    configure.Invoke(descriptor);

                    return this;
                }
            }
        }

        throw new Exception("Could not find vertex to bind to.");
    }
}
