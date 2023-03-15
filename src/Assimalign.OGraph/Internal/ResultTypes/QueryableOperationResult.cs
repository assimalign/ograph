
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

using Assimalign.OGraph.Syntax;

internal class QueryableOperationResult : IOGraphOperationResult
{
    
    public IOGraphPropertyContext PropertyResolverContext { get; set; }
    public IOGraphEdgeContext EdgeResolverContext { get; init; }
    public IQueryable Queryable { get; init; }

    public async Task ExecuteAsync(IOGraphResponse response, IOGraphOperationContext context, CancellationToken cancellationToken = default)
    {
        var graphQuery  = context.GetOGraphQuery();
        var graphNode   = context.GetOGraphNode();
      
        if (graphQuery is null)
        {
            response.StatusCode = 200;
            return;
        }
        if (graphType is null || graphType is not IOGraphComplexType complexType)
        {
            throw new Exception();
        }
        if (graphType.RuntimeType != Queryable.ElementType)
        {
            throw new Exception();
        }


        if (Query.Root is not RootNode root)
        {
            throw new Exception();
        }

        foreach (var item in Queryable)
        {
            foreach (var property in complexType.Properties)
            {
                var result = await property.GetResolverChain().Invoke(PropertyResolverContext);


                property.Type.

                result.
            }
        }
    }
}




public class OGrpahWriter
{



    void WriteStartObject() { }
    void WriteEndObject() { }
}