namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmSubscriber : IOGraphGdmBindableElement
{
    /// <summary>
    /// The event in which the subscriber is listening to.
    /// </summary>
    IOGraphGdmEvent Event { get; }

    /// <summary>
    /// The graph in which the event is emitted from.
    /// </summary>
    IOGraphGdmGraph Graph { get; }
}