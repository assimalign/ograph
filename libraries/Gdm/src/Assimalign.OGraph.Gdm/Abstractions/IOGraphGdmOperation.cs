namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmOperation : IOGraphGdmBindableElement
{
    /// <summary>
    /// Gets the operation type.
    /// </summary>
    GdmOperationKind Kind { get; }

    /// <summary>
    /// Gets the input type, if any.
    /// </summary>
    IOGraphGdmType? ReturnType { get; }

    /// <summary>
    /// Gets a collection of parameters for 
    /// </summary>
    IOGraphGdmParameterCollection Parameters { get; }
}