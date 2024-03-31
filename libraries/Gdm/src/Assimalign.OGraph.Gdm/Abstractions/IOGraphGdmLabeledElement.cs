namespace Assimalign.OGraph.Gdm;


public interface IOGraphGdmLabeledElement : IOGraphGdmElement
{
    /// <summary>
    /// The element label.
    /// </summary>
    Label Label { get; }
}