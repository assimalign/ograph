using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

internal class MissingProjectAnalyzer : QueryAnalyzer
{
    public override Task AnalyzeAsync(QueryAnalyzerContext context, CancellationToken cancellationToken = default)
    {
        return Task.Run(() =>
        {
            if (context.Document.Root is not null)
            {
                var vertices = context.Document.Root.GetNodesOfType<VertexNode>();

                foreach (var vertex in vertices)
                {
                    var projections = vertex.Nodes.OfType<ProjectNode>();

                    if (!projections.Any())
                    {
                        // TODO: Add Missing Project Statement Diagnostic
                    }
                    if (projections.Count() > 1)
                    {
                        // TODO: 
                    }
                }
            }
        });
        
    }
}
