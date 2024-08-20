using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;


/// <summary>
/// A command binding represents an operation that changes state.
/// </summary>
public interface IOGraphGdmCommandBinding : IOGraphGdmBinding
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
