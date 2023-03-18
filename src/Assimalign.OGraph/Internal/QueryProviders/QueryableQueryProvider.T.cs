
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

using Assimalign.OGraph.Syntax;

internal class OGraphQueryableQueryProvider<T> : IOGraphQueryProvider
{
    public Type ElementType => typeof(IQueryable<T>);

    public IQueryable<T> Queryable { get; init; }

    public Task<IOGraphQueryResult> ExecuteAsync(IOGraphQueryContext context, OGraphQueryOptions options, CancellationToken cancellationToken = default)
    {
        var query = context.Query.Root;
        var queryVisitor = new QueryExpressionVisitor(Queryable.Expression)
        {
            ElementType = Queryable.ElementType
        };

        var node = context.Node;

        var expression = queryVisitor.Visit(context.Query.Root);
        var expressionResult = Queryable.Provider.Execute(expression);

        if (expressionResult is not IEnumerable enumerable)
        {
            throw new Exception();
        }
        foreach (var item in enumerable)
        {
            var projections = query.OfType<RootNode>();

            if (projections.TryGetProjection(out var projection))
            {
                foreach (var edge in projection.Edges)
                {
                    if (node.Edges.TryGet(edge.GetEdgeName().Value, out var e))
                    {
                        
                    }
                }
            }
        }

        throw new NotImplementedException();
    }
}