using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphPropertyResult : IOGraphPropertyResult
{
    public OGraphPropertyResult()
    {
        
    }

    public OGraphPropertyResult(object data )
    {
        this.Data = data;
    }
    public bool IsSuccess => this.Error == null;
    public object Data { get; init; }
    public IOGraphError? Error { get; init; }
}
