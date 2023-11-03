using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Assimalign.OGraph.Syntax.Internal;

using Assimalign.OGraph.Syntax.Analyzer;
using System.Threading;

internal class QueryEdgeProjectionPropertyValidationAnalyzer : QueryAnalyzer
{
    private readonly IOGraph graph;

    public QueryEdgeProjectionPropertyValidationAnalyzer(IOGraph graph)
    {
        this.graph = graph;
    }

    public override Task AnalyzeAsync(QueryDocument document, CancellationToken cancellationToken)
    {
        foreach (var projection in document.Root.GetNodesOfType<ProjectionNode>())
        {
            
        }

        return Task.CompletedTask;
    }
}
