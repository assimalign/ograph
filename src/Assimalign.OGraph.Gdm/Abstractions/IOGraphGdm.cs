using System.IO;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// Represents a single graph Model.
/// </summary>
public interface IOGraphGdm
{
    /// <summary>
    ///The label of the Graph Model.
    /// </summary>
    /// <remarks>
    /// The label of the Graph model acts as a namespace. In terms a of a domain,
    /// there can be multiple models
    /// </remarks>
    Label Label { get; }
    /// <summary>
    /// Gets the collection of types in the graph model.
    /// </summary>
    IOGraphGdmTypeCollection Types { get; }
    /// <summary>
    /// Gets the edge collection.
    /// </summary>
    IOGraphGdmEdgeCollection Edges { get; }
    /// <summary>
    /// A collection of vertex definitions within the OGraph Model.
    /// </summary>
    IOGraphGdmVertexCollection Vertices { get; }

    //void SerializeToXml(Stream stream);
    //void SerializeToJson(Stream stream);
}