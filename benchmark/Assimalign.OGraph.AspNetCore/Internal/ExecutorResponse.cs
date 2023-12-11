using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.AspNetCore.Internal;

internal class ExecutorResponse : IOGraphExecutorResponse
{
    public ExecutorResponse(HttpContext context)
    {
        Body = context.Response.Body;
    }
    public StatusCode StatusCode { get; set; }
    public IOGraphExecutorHeaderCollection Headers { get; }
    public Stream Body { get; }
}
