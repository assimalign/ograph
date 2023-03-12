
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Execution;

using Assimalign.OGraph.Syntax;
using Assimalign.OGraph.Internal;
using Assimalign.OGraph.Execution.Internal;


public abstract class OGraphExecutor : IOGraphExecutor
{
    protected QueryParser? QueryParser { get; init; } = new QueryParser();

    protected IOGraph? Graph { get; init; }

    protected IServiceProvider? ServiceProvider { get; init; }


    protected OGraphOperationHandler Handler { get; set; }


    public virtual async Task<IOGraphResponse> ExecuteAsync(Name operationName, IOGraphRequest request, CancellationToken cancellationToken = default)
    {

        if (!TryGetOperation(operationName, out var operation))
        {
            // TODO: Response 404 - Not Found
            return new OGraphResponse()
            {
                StatusCode = 404
            };
        }

        var hasQuery = default(bool);

        if (!(hasQuery = TryGetQuery(request, out var query)))
        {
            if (!query.IsValid)
            {
                return new OGraphResponse()
                {
                    StatusCode = 400,
                };
            }
        }


        //var node = operation.Node;

        // Parse Query
        var context = new OGraphResolverContext()
        {
            ServiceProvider = this.ServiceProvider,
        };

        // Execute Operation
        try
        {
            var middlewares = operation.Middleware;
            var method = GetOperationChain(0, new OGraphOperationHandler(operation.Resolver.InvokeAsync));
            var result = await method.Invoke(context);


            OGraphOperationHandler GetOperationChain(int item, OGraphOperationHandler next)
            {
                var middleware = middlewares.SkipLast(item).First();
                var handler = new OGraphOperationHandler(ctx =>
                {
                    return middleware.InvokeAsync(ctx, next);
                });
                if (item < middlewares.Count - 1)
                {
                    return GetOperationChain(item + 1, handler);
                }

                return handler;
            }



        

            

        }
        catch (Exception exception) // TODO: Add a OGraph specific Callback cancellation exception. This will give the middleware a handle to invoke cancellation
        {
            // TODO: return bad result
        }
        // Build Execution Plan


        //// Execute Operation
        //var operationResult = await operation.Resolver.InvokeAsync(context);

        //if (operationResult.Data is IEnumerable enumerable)
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
        //        StatusCode = operationResult.StatusCode
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


    private bool TryGetOperation(Name name, out IOGraphOperation? operation)
    {
        operation = Graph.Operations.FirstOrDefault(x=>x.Name == name);

        



        return true;
    }

    private bool TryGetQuery(IOGraphRequest request, out QueryDocument? document)
    {
        document = null;


        if (request.Query.TryGetValue("query", out var query))
        {
            document = QueryParser.Parse(query);

            return true;
        }


        return false;
    }




    private OGraphOperationHandler GetMiddlewareChain()
    {

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
