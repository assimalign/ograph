namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmFunction : IOGraphGdmBindableElement
{
    /// <summary>
    /// Gets the input type, if any.
    /// </summary>
    IOGraphGdmTypeReference ReturnType { get; }

    /// <summary>
    /// A collection of parameters
    /// </summary>
    IOGraphGdmParameterCollection Parameters { get; }
}
