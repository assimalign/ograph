using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmGraph : IOGraphGdmElement
{
    /// <summary>
    /// The domain of the graph.
    /// </summary>
    GdmDomain Domain { get; }

    /// <summary>
    /// The root model.
    /// </summary>
    IOGraphGdm Model { get; }

    /// <summary>
    /// A collection of types defined within the graph.
    /// </summary>
    IOGraphGdmTypeCollection Types { get; }

    /// <summary>
    /// A collection of edges within the graph.
    /// </summary>
    IOGraphGdmEdgeCollection Edges { get; }

    /// <summary>
    /// A collection of nodes within the graph.
    /// </summary>
    IOGraphGdmNodeCollection Nodes { get; }

    /// <summary>
    /// A collection of events within the graph.
    /// </summary>
    IOGraphGdmEventCollection Events { get; }

    /// <summary>
    /// A collection of event subscribers within the graph.
    /// </summary>
    IOGraphGdmSubscriberCollection Subscribers { get; }
}