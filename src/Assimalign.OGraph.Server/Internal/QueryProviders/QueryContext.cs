using System;
using System.IO;

namespace Assimalign.OGraph.Internal.QueryProviders;

using Assimalign.OGraph.Gdm;
using Assimalign.OGraph.Syntax;

internal class QueryContext : IOGraphQueryProviderContext
{
    public IOGraphGdmVertex Vertex { get; init; } = default!;
    public QueryDocument Query { get; init; } = default!;
    public IServiceProvider ServiceProvider { get; init; } = default!;
    public Stream Stream { get; init; } = default!;

    public QueryDocument QueryDocument => throw new NotImplementedException();

    public IOGraphGdmElement Element => throw new NotImplementedException();
}
