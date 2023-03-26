
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

internal class QueryableQueryProvider<T> : IOGraphQueryProvider
{
    public Type ElementType => typeof(IQueryable<T>);

    public async Task<IOGraphQueryResult> ExecuteAsync(IOGraphQueryContext context, OGraphQueryOptions options, CancellationToken cancellationToken = default)
    {
        if (context is not QueryableQueryContext queryableContext)
        {
            throw new ArgumentException();
        }
        if (queryableContext.Queryable is not IQueryable<T> queryable)
        {
            throw new InvalidOperationException();
        }


        var result = new QueryResult();

        var query           = (RootNode)context.Query.Root;
        var queryVisitor    = new QueryExpressionVisitor(queryable.Expression);

        var node            = context.Node;
        var nodeType        = (IOGraphComplexType)context.Node.Type;

        var expression          = queryVisitor.Visit(context.Query.Root);

        var propertyContext = new OGraphPropertyContext()
        {
            ServiceProvider = queryableContext.ServiceProvider,
        };

        if (options.CanProject && query.TryGetProjection(out var projections))
        {
            foreach (var item in queryable)
            {
                var resultNode = new QueryResultNode();

                var tasks = new List<Task<Tuple<string, IOGraphPropertyResult>>>();

                propertyContext.Parent = item;

                foreach (var projectionProperty in projections.Properties)
                {
                    if (!nodeType.Properties.TryGet(projectionProperty.Name, out var graphProperty))
                    {
                        throw new Exception();
                    }

                    tasks.Add(Task.Run(async () =>
                    {
                        var value = await graphProperty.GetResolverChain().Invoke(propertyContext);

                        return new Tuple<string, IOGraphPropertyResult>(
                            projectionProperty.HasAlias ? projectionProperty.Alias : graphProperty.Name, 
                            value);
                    }));
                }
                while (tasks.Any())
                {
                    var any = await Task.WhenAny(tasks);

                    tasks.Remove(any);

                    var taskResult = any.Result;

                    resultNode.Add(taskResult.Item1, taskResult.Item2.Value);
                }

                result.Nodes.Add(resultNode);
            }
        }

        return result;
    }
}