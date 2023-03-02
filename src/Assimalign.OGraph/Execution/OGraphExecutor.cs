
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Execution;

using Assimalign.OGraph.Syntax;


public abstract class OGraphExecutor : IOGraphExecutor
{
    protected QueryParser QueryParser { get; }

    protected IOGraph GraphModel { get; }


    public virtual async Task<IOGraphResponse> ExecuteAsync(IOGraphRequest request, CancellationToken cancellationToken = default)
    {
        var operation = GraphModel.Operations.First();
        var node = operation.Node;

        // Parse Query
        




        // Execute Operation Middleware
        try
        {
            foreach (var middleware in operation.Middleware)
            {
                await middleware.InvokeAsync(default);
            }
        }
        catch(Exception exception) // TODO: Add a OGraph specific Callback cancellation exception. This will give the middleware a handle to invoke cancellation
        {
            // TODO: return bad result
        }


        // Execute Operation
        var operationResult = await operation.Resolver.InvokeAsync(default);

        // Load Result Strategy
        // TODO: The result strategy will dictate whether to apply OGraph query


        foreach (var property in node.Properties)
        {
            var propertyType = property.Type;
           // var propertyValue = propertyType.Resolver.Invoke(default);

           
        }



        throw new NotImplementedException();
    }





    internal abstract class OGraphQueryExecutionStrategy
    {

    }

}
