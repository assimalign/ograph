using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public sealed class OGraphError : IOGraphError
{
    public string Code { get; init; }
    public string Message { get; init; }
    public IOGraphErrorDetailsCollection Details { get; init; }
}
