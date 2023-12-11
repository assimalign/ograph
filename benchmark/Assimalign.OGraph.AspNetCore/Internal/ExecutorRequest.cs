using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.AspNetCore.Internal;

internal class ExecutorRequest : IOGraphExecutorRequest
{
    public ExecutorRequest(HttpContext context)
    {
        Host = context.Request.Host.Value;
        Path = context.Request.Path.Value!;
        Body = context.Request.Body;
        Method = context.Request.Method;

        context.Request.Headers
    }

    public Host Host { get; }
    public Path Path { get; }
    public Method Method { get; }
    public Stream Body { get; }
    public IOGraphExecutorQueryCollection Query { get; }
    public IOGraphExecutorHeaderCollection Headers { get; }
}
