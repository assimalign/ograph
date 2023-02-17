using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphOperationResult
{
    /// <summary>
    /// The status code of the OGraph operation to
    /// </summary>
    int StatusCode { get; }
}

