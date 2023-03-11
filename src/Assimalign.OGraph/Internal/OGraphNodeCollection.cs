using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


namespace Assimalign.OGraph.Internal;

internal class OGraphNodeCollection : IOGraphNodeCollection
{ 

    private readonly List<IOGraphNode> nodes;

    public OGraphNodeCollection()
    {
        this.nodes = new List<IOGraphNode>();
    }

    public bool IsReadOnly { get; set; }
    public int Count => nodes.Count;

    public bool TryFind(Label label)
    {
        return nodes.Any(node => node.Label == label);
    }
    public bool TryGet(Label Label, out IOGraphNode? node)
    {
        node = nodes.FirstOrDefault(x => x.Label == Label);

        return node is null ? false : true;
    }

    public void Add(IOGraphNode node)
    {
        AssertReadOnly();

        if (node is null)
        {
            throw new ArgumentNullException(nameof(node));
        }
        if (this.Any(x => x.Label == node.Label))
        {
            throw new ArgumentException($"The node with the label '{node.Label}' already exists.");
        }

        nodes.Add(node);
    }
    public void Remove(IOGraphNode node)
    {
        AssertReadOnly();

        if (node is null)
        {
            throw new ArgumentNullException(nameof(node));
        }

        nodes.Remove(node);
    }

    public IEnumerator<IOGraphNode> GetEnumerator() => this.nodes.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();


    private void AssertReadOnly()
    {
        if (IsReadOnly)
        {
            throw new InvalidOperationException("The Collection is ReadOnly.");
        }
    }
}
