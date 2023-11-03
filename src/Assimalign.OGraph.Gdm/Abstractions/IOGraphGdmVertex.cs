using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// Represents a single entity and it's structure within the graph Model.
/// </summary>
/// <remarks>
/// A Node is also referred to as a Vertex.
/// </remarks>
public interface IOGraphGdmVertex
{
    /// <summary>
    /// Represents the label each node should contain.
    /// </summary>
    Label Label { get; }
    /// <summary>
    /// The type bound to this vertex.
    /// </summary>
    IOGraphGdmTypeReference Type { get; } // TODO: Revisit whether IOGraphEntityType should be specified explicitly
    /// <summary>
    /// A collection of edges that are connected to this node.
    /// </summary>
    IOGraphGdmEdgeReferenceCollection Edges { get; }
    /// <summary>
    /// Represents arbitrary metadata that can be associated with this node.
    /// </summary>
    IOGraphGdmMetadata Metadata { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerable<IOGraphGdmBinding> GetBindings();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="binding"></param>
    /// <returns></returns>
    void AddBinding(IOGraphGdmInputBinding binding);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="binding"></param>
    /// <returns></returns>
    void AddBinding(IOGraphGdmOutputBinding binding);
}