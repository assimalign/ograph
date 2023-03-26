using Assimalign.OGraph.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class QueryableQueryContext : IOGraphQueryContext
{
    public IQueryable Queryable { get; init; }
    public IOGraphNode Node { get; init; }
    public QueryDocument Query { get; init; }
    public IServiceProvider ServiceProvider { get; init; }
}
