using Assimalign.OGraph.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class QueryProviderContextDefault : IOGraphQueryContext
{
    public IOGraphNode Node { get; init; }
    public QueryDocument Query { get; init; }
    public IServiceProvider ServiceProvider { get; init; }

    public Stream Stream => throw new NotImplementedException();
}
