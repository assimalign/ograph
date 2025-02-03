namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmComplexType : IOGraphGdmType
{
    /// <summary>
    /// A collection of properties
    /// </summary>
    IOGraphGdmPropertyCollection Properties { get; }

    /// <summary>
    /// A ollection of functions.
    /// </summary>
    IOGraphGdmFunctionCollection Functions { get; }
}