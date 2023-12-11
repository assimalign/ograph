using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OperationBindingQueryParsingMiddleware : IOGraphOperationBindingMiddleware
{
    public Task<IOGraphResult> InvokeAsync(
        IOGraphOperationBindingContext context, 
        CancellationToken cancellationToken, 
        OGraphOperationBindingMiddlewareHandler next)
    {
        try
        {
            if (context is not OperationBindingContext ctx)
            {
                throw new Exception();
            }

            var request = ctx.Request;
            
            // 1. Check for query
            if (request.Query.TryGetValue(QueryKey.Query, out var queryValue))
            {
                var queryParser = ctx.Parser;
                var queryDocument = queryParser.Parse(queryValue);

                // Check Query Validation
                if (!queryDocument.IsValid)
                {

                }
            }

            return next.Invoke(context, cancellationToken);
        }
        catch (Exception exception)
        {
            throw;
        }
    }
}
