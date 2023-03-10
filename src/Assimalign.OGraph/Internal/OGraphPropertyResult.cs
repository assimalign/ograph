using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphPropertyResult : IOGraphPropertyResult{

    public OGraphPropertyResult(object data )
    {
        this.Data = data;
    }
    public object Data { get; }
}
