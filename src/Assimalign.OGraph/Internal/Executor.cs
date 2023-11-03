
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

using Assimalign.OGraph.Gdm;

internal class Executor : IOGraphExecutor
{
    IOGraphGdm Model { get; } = default!;

    public async Task<IOGraphExecutorResponse> ExecuteAsync(IOGraphExecutorRequest request, CancellationToken cancellationToken = default)
    {
        var vertex = Model.Vertices.First();
        var vertexBindings = vertex.GetBindings()
            .OfType<IOGraphOperation>();

        if (vertexBindings.Count() > 1)
        {
            throw new Exception("Only one output binding is allowed");
        }

        var vertexBinding = vertexBindings.First();

        var result = await vertexBinding.InvokeAsync(default!);


       


        throw new NotImplementedException();
    }
}
