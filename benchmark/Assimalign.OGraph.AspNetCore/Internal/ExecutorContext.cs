using System;
using System.Security.Claims;

namespace Assimalign.OGraph.AspNetCore.Internal;

using Microsoft.AspNetCore.Http;

internal class ExecutorContext : IOGraphExecutorContext
{
    public ExecutorContext(HttpContext context)
    {
        ClaimsPrincipal = context.User;
        ServiceProvider = context.RequestServices;
        Request = new ExecutorRequest(context);
        Response = new ExecutorResponse(context);
    }
    public IOGraphRequest Request { get; }
    public IOGraphResponse Response { get; }
    public IServiceProvider ServiceProvider { get; }
    public ClaimsPrincipal ClaimsPrincipal { get; }
}