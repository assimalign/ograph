using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmOperationBinding : IOGraphGdmBinding
{
    /// <summary>
    /// Get the kind of operation being invoked.
    /// </summary>
    GdmOperationKind Kind { get; }
    /// <summary>
    /// Gets the input type, if any.
    /// </summary>
    IOGraphGdmTypeReference Input { get; }
    /// <summary>
    /// Gets the output type, if any.
    /// </summary>
    IOGraphGdmTypeReference Output { get; }

    IEnumerable<IOGraphGdmBindingParameter> Parameters { get; }
}
