using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Execution;

public sealed class OGrpahResult
{


    public static IOGraphOperationResult Ok<TData>(TData data)
    {
        return new OGraphOperationResult<TData>()
        {
            Data = data,
            StatusCode = 200
        };
    }
}
