using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public sealed class OGraphPropertyResult : IOGraphPropertyResult
{
    public object Data { get; init; }

    public bool IsSuccess => this.Error is null;

    public IOGraphError? Error { get; init; }


}
