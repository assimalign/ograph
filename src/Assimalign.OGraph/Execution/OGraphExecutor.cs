
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Execution;

using Assimalign.OGraph.Syntax;
using Assimalign.OGraph.Internal;


public abstract class OGraphExecutor : IOGraphExecutor
{
    protected QueryParser QueryParser { get; }

    protected IOGraph GraphModel { get; }


    public virtual async Task<IOGraphResponse> ExecuteAsync(IOGraphRequest request, CancellationToken cancellationToken = default)
    {
        if (!TryGetOperation(request, out var operation))
        {
            // TODO: Response 404 - Not Found
        }
        if (!TryGetQuery(request, out var query))
        {
            // TODO: Response 400 - Bad Request
        }


        var node = operation.Node;

        // Parse Query
        

        var context = new OGraphResolverContext();



        // Execute Operation Middleware
        try
        {
            foreach (var middleware in operation.Middleware)
            {
                await middleware.InvokeAsync(context);
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

            

            //property.Resolver.


           // var propertyValue = propertyType.Resolver.Invoke(default);

           
        }



        throw new NotImplementedException();
    }


    private bool TryGetOperation(IOGraphRequest request, out IOGraphOperation operation)
    {
        operation = default;




        return false;
    }

    private bool TryGetQuery(IOGraphRequest request, out QueryDocument document)
    {
        document = default;



        return false;
    }



}
