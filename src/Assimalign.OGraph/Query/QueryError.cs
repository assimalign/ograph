using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public readonly struct QueryError : IOGraphError
{
    public string? Code => throw new NotImplementedException();

    public string? Message => throw new NotImplementedException();

    public IOGraphErrorDetailsCollection? Details => throw new NotImplementedException();
}
