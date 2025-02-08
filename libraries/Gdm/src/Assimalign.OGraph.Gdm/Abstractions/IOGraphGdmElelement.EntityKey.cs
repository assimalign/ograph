namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmEntityKey : IOGraphGdmElement
{
    /// <summary>
    /// Gets the property reference of the Entity Key.
    /// </summary>
    IOGraphGdmProperty Property { get; }
}
