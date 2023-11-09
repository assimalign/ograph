using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

using Assimalign.OGraph.Gdm;

internal class OperationBindingContext : IOGraphOperationBindingContext
{
    public IOGraphGdmVertex Element { get; init; } = default!;
    public IOGraphExecutorRequest Request { get; init; } = default!;
    public IOGraphExecutorResponse Response { get; init; } = default!;

    IOGraphGdmElement IOGraphGdmBindingContext.Element => Element;
}
