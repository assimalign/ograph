namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmEvent : IOGraphGdmBindableElement
{
    /// <summary>
    /// 
    /// </summary>
    IOGraphGdmType? OutputType { get; }

    /// <summary>
    /// The graph in which the event is emitted from.
    /// </summary>
    IOGraphGdmGraph Graph { get; }
}
