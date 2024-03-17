
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;


internal class UnknownRootVertexNodeAnalyzer : QueryAnalyzer
{
    public override Task AnalyzeAsync(QueryDocument document, CancellationToken cancellationToken = default)
    {
        return Task.Run(() =>
        {
            //var vertex = document.Root?.Vertex;

            //if (vertex is { Label: not null, Label.Name: not null, Label.Name.Length: > 0 })
            //{
            //    // TODO: Add Unknown Vertex Diagnostics
            //}
        });
    }
}
