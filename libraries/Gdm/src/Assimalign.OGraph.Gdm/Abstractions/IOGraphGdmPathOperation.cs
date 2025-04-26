namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmPathOperation : IOGraphGdmOperation
{
    /// <summary>
    /// A reference to the path the operation is bound to.
    /// </summary>
    IOGraphGdmPath Path { get; }
}
