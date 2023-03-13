using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Assimalign.OGraph.Internal;

internal class OGraphEdgeCollection : IOGraphEdgeCollection
{
    private readonly List<IOGraphEdge> edges;

    public OGraphEdgeCollection()
    {
        this.edges = new List<IOGraphEdge>();
    }

    public bool IsReadOnly { get; set; }
    public int Count => edges.Count;

    public bool TryFind(Name name)
    {
        return edges.Any(node => node.Name == name);
    }
    public bool TryGet(Name name, out IOGraphEdge? edge)
    {
        edge = edges.FirstOrDefault(x => x.Name == name);

        return edge is null ? false : true;
    }
    public void Add(IOGraphEdge edge)
    {
        AssertReadOnly();

        if (edge is null)
        {
            throw new ArgumentNullException(nameof(edge));
        }
        if (this.Any(x => x.Name == edge.Name && x.Source.Label == edge.Source.Label && x.Target.Label == edge.Target.Label))
        {
            throw new ArgumentException($"An edge with the Name '{edge.Name}' already exists between '{edge.Source.Label}' -> '{edge.Target.Label}'.");
        }

        edges.Add(edge);
    }
    public void Remove(IOGraphEdge edge)
    {
        AssertReadOnly();

        if (edge is null)
        {
            throw new ArgumentNullException(nameof(edge));
        }

        edges.Remove(edge);
    }

    public IEnumerator<IOGraphEdge> GetEnumerator() => this.edges.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();


    private void AssertReadOnly()
    {
        if (IsReadOnly)
        {
            throw new InvalidOperationException("The Collection is ReadOnly.");
        }
    }
}
