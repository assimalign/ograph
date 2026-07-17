using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Server.Executor;

internal class TestExecutorContext : IOGraphExecutorContext
{
    public IOGraphExecutorRequest Request { get; init; }
    public IOGraphExecutorResponse Response { get; init; }
    public IServiceProvider? ServiceProvider { get; init; }
    public ClaimsPrincipal ClaimsPrincipal { get; init; } 
}
