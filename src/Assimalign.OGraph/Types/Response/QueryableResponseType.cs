using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

internal class QueryableResponseType : IOGraphResponseType
{
    public Name TypeName => throw new NotImplementedException();

    public OGraphTypeIdentifier TypeIdentifier => throw new NotImplementedException();

    public Type? RuntimeType => throw new NotImplementedException();

    public IQueryable Queryable { get; init; }

    public Task SerializeJsonAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task SerializeXmlAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
