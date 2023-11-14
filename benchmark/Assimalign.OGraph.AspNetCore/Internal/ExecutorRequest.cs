using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.AspNetCore.Internal;

internal class ExecutorRequest : IOGraphRequest
{
    public ExecutorRequest(HttpContext context)
    {
        Host = context.Request.Host;
    }

    public Host Host { get; }

    public Path Path => throw new NotImplementedException();

    public Method Method => throw new NotImplementedException();

    public Stream Body => throw new NotImplementedException();

    public IOGraphQueryCollection Query => throw new NotImplementedException();

    public IOGraphHeaderCollection Headers => throw new NotImplementedException();
}
