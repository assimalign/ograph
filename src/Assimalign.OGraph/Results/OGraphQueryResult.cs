using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public sealed class OGraphQueryResult : OGraphResult
{
    private readonly IOGraphQueryResult queryResult;

    public OGraphQueryResult(IOGraphQueryResult queryResult)
    {
        if (queryResult is null)
        {
            throw new ArgumentNullException(nameof(queryResult));
        }

        this.queryResult = queryResult;
    }


    public override Task ExecuteAsync(IOGraphExecutorContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            context.Response.StatusCode = 200;

            var content     = context.Response.Body;
            var contentType = context.Request.Headers.ContentType;


            return JsonSerializer.SerializeAsync(content, queryResult);

        }
        catch(Exception exception)
        {

        }

        return Task.CompletedTask;
    }
}
