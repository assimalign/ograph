namespace Assimalign.OGraph;

using Assimalign.OGraph.Gdm;

public interface IOGraphContext
{
    /// <summary>
    /// Gets the Graph Data Model.
    /// </summary>
    IOGraphGdm Model { get; }
}
