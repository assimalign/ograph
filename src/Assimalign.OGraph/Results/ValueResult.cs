using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

internal class ValueResult : IOGraphPropertyResult
{
    public object Value { get; init; }

    public StatusCode StatusCode => throw new NotImplementedException();
}
