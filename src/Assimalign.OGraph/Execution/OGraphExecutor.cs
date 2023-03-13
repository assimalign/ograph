
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Execution;

using Assimalign.OGraph.Syntax;
using Assimalign.OGraph.Internal;
using Assimalign.OGraph.Execution.Internal;

public abstract class OGraphExecutor : IOGraphExecutor
{
    protected virtual QueryParser? QueryParser { get; init; } = new QueryParser();
    protected virtual IOGraph? Graph { get; init; }
    protected virtual IServiceProvider? ServiceProvider { get; init; }


    public virtual async Task<IOGraphResponse> ExecuteAsync(Name name, IOGraphRequest request, CancellationToken cancellationToken = default)
    {
        var response = new OGraphResponse();

        if (!TryGetOperation(name, out var operation))
        {
            // TODO: Response 404 - Not Found
            response.StatusCode = 404;

            return response;
        }

        // Check for OGraph Query then validate.
        var hasQuery = default(bool);
        if (hasQuery = TryGetQuery(request, out var query))
        {
            if (!query.IsValid)
            {
                return new OGraphResponse()
                {
                    StatusCode = 400,
                };
            }
        }

        var context = new OGraphResolverContext()
        {
            ServiceProvider = this.ServiceProvider,
            RequestBody = request.Body,
            RequestQuery = request.Query,
            RequestHeaders = request.Headers,
        };

        // Execute Operation
        try
        {
            var result = await operation.GetResolverChain().Invoke(context);

            if (result.IsSuccess)
            {
                //var content = result.Data;

                //if (hasQuery)
                //{
                //    var queryContext = new OGraphQueryContext()
                //    {
                //        Graph = this.Graph,
                //        Node = operation.Node,
                //        Options = new OGraphQueryOptions(),
                //    };

                //    if (query.Root is not RootQueryNode root)
                //    {
                //        throw new Exception();
                //    }
                //    if (root.TryGetFilters(out var filters))
                //    {
                //        var filter = filters.First(x => !x.HasEdge);


                //        if (!filters.Any(x=>x.HasEdge))
                //        {

                //        }
                //    }

                //    operation.QueryProvider.ApplyFiltering()

                //    var queryProvider = new QueryableQueryProvider(queryContext);

                //    content = queryProvider.Execute(content);
                //}


            }
        }
        catch (Exception exception) // TODO: Add a OGraph specific Callback cancellation exception. This will give the middleware a handle to invoke cancellation
        {
            // TODO: return internal server error
        }
        // Build Execution Plan

        var node = operation.Node;

        //if (query.Root.GetNodesOfType<RootQueryNode>().First().TryGetProjections(out var projections))
        //{
        //    var rootProjections = projections.FirstOrDefault(x => !x.HasEdge);

        //    foreach (var property in rootProjections.Properties)
        //    {
        //        if (!node.Properties.TryGet(property.Name, out var propertyValue))
        //        {
        //            throw new Exception();
        //        }
        //        if (propertyValue.Type.TypeIdentifier == OGraphTypeIdentifier.Primitive)
        //        {

        //        }
        //        var result = await propertyValue.Resolver.InvokeAsync(context);

        //    }
        //}



        //// Execute Operation
        //var result = await operation.Resolver.InvokeAsync(context);

        //if (result.Data is IEnumerable enumerable)
        //{
        //    var data = new List<Dictionary<string, object>>();

        //    foreach (var item in enumerable)
        //    {
        //        context.Parent = item;


        //        var resolverTasks = new Dictionary<string, Task<IOGraphPropertyResult>>();
        //        // Load Result Strategy
        //        // TODO: The result strategy will dictate whether to apply OGraph query

        //        foreach (var projection in query.Root.GetNodesOfType<ProjectionQueryNode>())
        //        {
        //            foreach (var property in projection.Properties)
        //            {
        //                Name name = property.Name;

        //                var graphProperty = node.Properties.Find(property.Name);

        //                resolverTasks.Add(property.Name, graphProperty.Resolver.InvokeAsync(context).AsTask());
        //            }
        //        }


        //        var results = await Task.WhenAll(resolverTasks.Values);

        //        data.Add(resolverTasks.ToDictionary(p => p.Key, p => p.Value.Result.Data));


        //    }


        //    var response = new OGraphResponse()
        //    {
        //        StatusCode = result.StatusCode
        //    };


        //    await JsonSerializer.SerializeAsync(response.Body, data, cancellationToken: cancellationToken);

        //    response.Body.Position = 0;


        //    return response;

        //}





        //foreach (var property in node.Properties)
        //{
        //    var propertyType = property.Type;



        //    //property.Resolver.


        //   // var propertyValue = propertyType.Resolver.Invoke(default);


        //}



        throw new NotImplementedException();
    }


    private bool TryGetOperation(Name operationName, out IOGraphOperation? operation)
    {
        operation = Graph.Operations.FirstOrDefault(x => x.Name == operationName);

        return operation is null ? false : true;
    }

    private bool TryGetQuery(IOGraphRequest request, out QueryDocument? document)
    {
        document = null;

        try
        {
            if (request.Query.TryGetValue("query", out var query))
            {
                document = QueryParser.Parse(query);

                return true;
            }
            return false;
        }
        catch
        {
            return false;
        }        
    }



    public static IOGraphExecutor Create(IOGraph graph)
    {
        return new OGraphExecutorDefault
        {
            Graph = graph,
        };
    }








}

internal class OGraphExecutorDefault : OGraphExecutor
{

}
