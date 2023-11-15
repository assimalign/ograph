using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Internal;

using Assimalign.OGraph.Syntax.Analyzer;

/*
 This analyzer ensure that the only nodes in a Vertex Node are valid.
 */
internal class InvalidNodeTypesInVertexAnalyzer : QueryAnalyzer
{
    public override Task AnalyzeAsync(QueryDocument document, CancellationToken cancellationToken = default)
    {
        return Task.Run(() =>
        {
            var root = document.Root;

            foreach (var vertexNode in root.GetNodesOfType<VertexNode>())
            {
                foreach (var node in vertexNode.Nodes)
                {
                    // These are the only acceptable nodes in 
                    if (node is not ProjectionNode &&
                        node is not FilterNode &&
                        node is not PageNode &&
                        node is not SortNode)
                    {
                        document.AddDiagnostic(new Diagnostic()
                        {

                        });
                    }
                }
            }
        });
    }
}