namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmOperation : IOGraphGdmBindableElement
{
    /// <summary>
    /// Get's the operation type.
    /// </summary>
    GdmOperationKind Kind { get; }

    /// <summary>
    /// Gets the input type, if any.
    /// </summary>
    IOGraphGdmTypeReference ReturnType { get; }

    /// <summary>
    /// Gets a collection of parameters for 
    /// </summary>
    IOGraphGdmParameterCollection Parameters { get; }
}
