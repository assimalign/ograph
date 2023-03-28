
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
using System.Reflection;

internal class QueryableQueryProvider<T> : IOGraphQueryProvider
{
    public Type ElementType => typeof(IQueryable<T>);



    Task<IOGraphQueryResult> IOGraphQueryProvider.ExecuteAsync(IOGraphQueryContext context, OGraphQueryOptions options, CancellationToken cancellationToken)
    {
        if (context is QueryableQueryContext ctx)
        {
            return ExecuteAsync(ctx, options, cancellationToken);
        }

        throw new ArgumentException();
    }


    public async Task<IOGraphQueryResult> ExecuteAsync(QueryableQueryContext context, OGraphQueryOptions options, CancellationToken cancellationToken = default)
    {
        if (context.Queryable is not IQueryable<T> queryable)
        {
            throw new InvalidOperationException();
        }

        var result = new QueryResult();
        var query = (RootNode)context.Query.Root;
        var queryVisitor = new QueryableQueryVisitor<T>(queryable);
        var node = context.Node;
        var nodeType = (IOGraphComplexType)context.Node.Type;

        var propertyContext = new OGraphPropertyContext()
        {
            ServiceProvider = context.ServiceProvider
        };
        if (options.CanFilter && query.TryGetFilter(out var filterNode))
        {
            queryable = ApplyFiltering(queryable, options, filterNode);
        }
        if (options.CanPage && (query.TryGetPage(out var pageNode) || options.DefaultPageSize.HasValue))
        {
            queryable = ApplyPaging(queryable, options, pageNode ??= new PageNode(options.DefaultPageSize.Value, 0));
        }
        if (options.CanSort && query.TryGetSort(out var sortNode))
        {
            queryable = ApplySorting(queryable, options, sortNode);
        }
        if (options.CanProject && query.TryGetProjection(out var projections))
        {
            foreach (var item in queryable)
            {
                var resultNode = new QueryResultNode();
                var edgeTasks = new List<Task<Tuple<string, IOGraphEdgeResult>>>();
                var propertyTasks = new List<Task<Tuple<string, IOGraphPropertyResult>>>();

                propertyContext.Parent = item;

                foreach (var projectionProperty in projections.Properties)
                {
                    if (!nodeType.Properties.TryGet(projectionProperty.Name, out var graphProperty))
                    {
                        throw new Exception();
                    }

                    propertyTasks.Add(Task.Run(async () =>
                    {
                        var value = await graphProperty
                            .GetResolverChain()
                            .Invoke(propertyContext);

                        return new Tuple<string, IOGraphPropertyResult>(
                            projectionProperty.HasAlias ? projectionProperty.Alias : graphProperty.Name,
                            value);
                    }));
                }
                foreach (var edge in projections.Edges)
                {
                    var edgeNode = (EdgeNode)edge.Identifier;

                    if (node.Edges.TryGet(edgeNode.GetEdgeName().Value, out var edgeResult))
                    {

                    }
                }
                while (propertyTasks.Any())
                {
                    var task = await Task.WhenAny(propertyTasks);
                    propertyTasks.Remove(task);
                    var taskResult = task.Result;

                    resultNode.Add(taskResult.Item1, taskResult.Item2.Value);
                }

                result.Nodes.Add(resultNode);
            }
        }

        return result;
    }


    private async Task<QueryResult> GetResultAsync(
        IQueryable<T> queryable,
        IOGraphNode graphNode,
        ProjectionNode projectionNode,
        QueryableQueryContext context,
        OGraphQueryOptions options)
    {
        var queryResults = new QueryResult();
        var queryCount = queryable.Count();
        var propertyContext = new OGraphPropertyContext()
        {
            ServiceProvider = context.ServiceProvider
        };

        if (context.Node.Type is not IOGraphComplexType complexType)
        {
            throw new Exception();
        }
        foreach (var item in queryable)
        {
            // Set the current iterating parent
            propertyContext.Parent = item;

            foreach (var projection in projectionNode.Properties)
            {
                var propertyName = projection.Name;                
                if (!complexType.Properties.TryGet(propertyName, out var property))
                {
                    throw new Exception();
                }
                if (projection.HasChildren)
                {

                }
                var propertyTask = property.GetResolverChain().Invoke(propertyContext);

            }
        }

        return queryResults;
    }
    private async Task<QueryResult> GetEdgeResultAsync()
    {
        var queryResults = new QueryResult();






        return queryResults;
    }


    private IQueryable<T> ApplyPaging(IQueryable<T> queryable, OGraphQueryOptions options, PageNode pageNode)
    {
        var skip = pageNode.Skip is not null && pageNode.Skip.TryGetInt32(out var s) ? s : 0;
        var take = pageNode.Take is not null && pageNode.Take.TryGetInt32(out var t) ? t : options.DefaultPageSize;

        if (options.MaxPageSize.HasValue && options.MaxPageSize.Value > take)
        {
            throw new Exception();
        }

        var skipExpression = Expression.Call(
            typeof(Queryable),
            "Skip",
            new Type[] { queryable.ElementType },
            queryable.Expression,
            Expression.Constant(skip));

        var takeExpression = Expression.Call(
            typeof(Queryable),
            "Take",
            new Type[] { queryable.ElementType },
            skipExpression,
            Expression.Constant(take));

        return queryable.Provider.CreateQuery<T>(takeExpression);
    }
    private IQueryable<T> ApplySorting(IQueryable<T> queryable, OGraphQueryOptions options, SortNode sortNode)
    {
        return Apply(queryable, sortNode);

        IQueryable<T> Apply(IQueryable<T> source, SortNode sortNode, int index = 0)
        {
            var parameterExpression = Expression.Parameter(typeof(T));
            var memberExpression = Expression.Property(parameterExpression, sortNode.Identifier.Name);

            string method;
            if (index == 0)
            {
                method = sortNode.Direction == SortDirection.Ascending ?
                    "OrderBy" :
                    "OrderByDescending";
            }
            else
            {
                method = sortNode.Direction == SortDirection.Ascending ?
                    "ThenBy" :
                    "ThenByDescending";
            }

            var lambda = Expression.Lambda<Func<T, object>>(
                memberExpression.Type.IsValueType == true ? Expression.Convert(memberExpression, typeof(object)) : memberExpression,
                parameterExpression);

            var linq = Expression.Call(
                typeof(Queryable),
                method,
                new Type[] { source.ElementType, typeof(object) },
                source.Expression,
                lambda);

            source = source.Provider.CreateQuery<T>(linq);

            if (null != sortNode.ThenBy)
            {
                return Apply(source, sortNode.ThenBy, index + 1);
            }

            return source;
        }
    }
    private IQueryable<T> ApplyFiltering(IQueryable<T> queryable, OGraphQueryOptions options, FilterNode filterNode)
    {


        return queryable;
    }
}