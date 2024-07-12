using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmQueryBinding : IOGraphGdmBinding
{
    /// <summary>
    /// Gets the input type, if any.
    /// </summary>
    IOGraphGdmTypeReference ReturnType { get; }
    /// <summary>
    /// 
    /// </summary>
    IEnumerable<IOGraphGdmBindingParameter> Parameters { get; }
}
