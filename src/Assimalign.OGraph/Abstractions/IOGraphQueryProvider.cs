
using System;

using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Assimalign.OGraph;

using Assimalign.OGraph.Syntax;


public interface IOGraphQueryProvider
{


    Type ElementType { get; }


    Task<IOGraphQueryResult> ExecuteAsync(IOGraphQueryContext context, OGraphQueryOptions options, CancellationToken cancellationToken = default);
}