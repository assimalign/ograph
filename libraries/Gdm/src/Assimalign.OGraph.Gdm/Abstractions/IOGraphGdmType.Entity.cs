namespace Assimalign.OGraph.Gdm;

/// <summary>
/// Represents a GdmEntity.
/// </summary>
public interface IOGraphGdmEntityType : IOGraphGdmComplexType
{
    /// <summary>
    /// 
    /// </summary>
    IOGraphGdmEntityKey Key { get; }
}