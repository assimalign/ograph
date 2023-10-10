using Assimalign.OGraph.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class QueryableQueryProvider : QueryProviderDefault
{
    public override Type ElementType { get; init; }

    public override Task ExecuteAsync(QueryProviderContextDefault context, OGraphQueryOptions options, CancellationToken cancellationToken = default)
    {
        var graphNode = context.Node;
        var grpahType = graphNode.Type as IOGraphComplexType;
     
        if (!graphNode.Type.RuntimeType.IsAssignableTo(ElementType))
        {
            throw new Exception("Type mismatch");
        }

        var node = context.Query.Root as VertexNode;

        if (node.TryGetProjection(out var projections))
        {
            foreach (var identifier in projections.Properties)
            {
                if (grpahType!.Properties.TryGet(identifier.Name, out var graphProperty)) 
                {
                    var handler = graphProperty.BuildHandlerChain();

                    handler.
                }
                else
                {
                    throw new Exception("Invalid projections");
                }
            }
        }

        throw new NotImplementedException();
    }
}
