namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmFunction : IOGraphGdmMember
{
    /// <summary>
    /// Gets the input type, if any.
    /// </summary>
    IOGraphGdmType ReturnType { get; }

    /// <summary>
    /// A collection of parameters
    /// </summary>
    IOGraphGdmParameterCollection Parameters { get; }
}
