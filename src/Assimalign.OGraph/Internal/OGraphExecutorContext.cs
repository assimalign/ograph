using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphExecutorContext : IOGraphExecutorContext
{
    public string ContentType { get; init; }
    public IOGraphExecutorRequest Request { get; init; }
    public IOGraphExecutorResponse Response { get; init; }
}
