namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmLabeledElement : IOGraphGdmElement
{
    /// <summary>
    /// The element label.
    /// </summary>
    Label Label { get; }
}