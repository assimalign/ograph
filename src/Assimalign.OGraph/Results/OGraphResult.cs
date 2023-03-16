using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public abstract class OGraphResult : IOGraphOperationResult, IOGraphEdgeResult
{
    public abstract IOGraphError Error { get; }
}