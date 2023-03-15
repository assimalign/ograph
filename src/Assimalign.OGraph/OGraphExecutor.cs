
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Internal;
using Assimalign.OGraph.Syntax;

public class OGraphHttpExecutor : IOGraphExecutor
{
    private static readonly ConcurrentDictionary<Type, IQueryProvider> providers;


    protected virtual IOGraph Graph { get; init; }

    public virtual async Task<IOGraphResponse> ExecuteAsync(IOGraphRequest request, CancellationToken cancellationToken = default)
    {
        var response = default(IOGraphResponse);
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

            await result.ExecuteAsync(context, cancellationToken);

            return response;
        }
        catch (Exception exception)
        {
            return default(IOGraphResponse);
        }
    }

    public bool TryValidate(IOGraphRequest request, out IOGraphError error)
    {
        error = default;

        // 1. Validate Client Accept Header
        // 2. Validate Server Supported Media Types



        return true;

    }
    public bool TryGetOperation(IOGraphRequest request, out IOGraphOperation operation)
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

    public bool TryGetQuery(IOGraphRequest request, out QueryDocument queryDocument)
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
