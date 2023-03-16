
using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Internal;
using Assimalign.OGraph.Syntax;


public class OGraphHttpExecutor : IOGraphHttpExecutor
{
    protected virtual IOGraph Graph { get; init; }

    public virtual async Task<IOGraphHttpResponse> ExecuteAsync(IOGraphHttpRequest request, CancellationToken cancellationToken = default)
    {
        var response = default(IOGraphHttpResponse);
        try
        {
            if (!TryValidate(request, out var error))
            {

                return response;
            }
            if (!TryGetOperation(request, out var operation))
            {
                // TODO: Return Not Found Response
                return response;
            }
            var hasQuery = default(bool);
            if (hasQuery = TryGetQuery(request, out var query))
            {
                if (!query.IsValid)
                {
                    // TODO : Bad Query
                    return response;
                }
            }
            var context = new OGraphResolverContext()
            {

            };
            var result = await operation.GetResolverChain().Invoke(context);

            await JsonSerializer.SerializeAsync(response.Body, result);

            return response;
        }
        catch (Exception exception)
        {
            return default(IOGraphHttpResponse);
        }
    }

    public bool TryValidate(IOGraphHttpRequest request, out IOGraphError error)
    {
        error = default;

        // 1. Validate Client Accept Header
        // 2. Validate Server Supported Media Types



        return true;

    }
    public bool TryGetOperation(IOGraphHttpRequest request, out IOGraphOperation operation)
    {
        operation = default;

        foreach (var opr in Graph.Operations)
        {
            if (opr.Method == request.Method && opr.Route.IsMatch(request.Path))
            {
                operation = opr;
                return true;
            }
        }

        return false;
    }

    public bool TryGetQuery(IOGraphHttpRequest request, out QueryDocument queryDocument)
    {
        queryDocument = default;

        if (request.Query.TryGetValue("query", out var value))
        {
            queryDocument = new QueryParser().Parse(value);
            return true;
        }

        return false;
    }
}
