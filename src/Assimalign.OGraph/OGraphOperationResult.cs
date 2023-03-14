using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public sealed class OGraphOperationResult<TData> : IOGraphOperationResult
{
    public int StatusCode { get; set; }
    public TData Data { get; set; }
    object IOGraphOperationResult.Data => this.Data;

    public bool IsSuccess => throw new NotImplementedException();

    public IOGraphError? Error => throw new NotImplementedException();

}
