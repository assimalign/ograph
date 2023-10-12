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

    public bool TryFind(Name label)
    {
        return nodes.Any(node => node.Label == label);
    }
    public bool TryGet(Name label, out IOGraphNode? node)
    {
        node = nodes.FirstOrDefault(x => x.Label == label);

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

    public bool TryGetNode(Name name, out IOGraphNode? node)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public bool Contains(IOGraphNode item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(IOGraphNode[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    bool ICollection<IOGraphNode>.Remove(IOGraphNode item)
    {
        throw new NotImplementedException();
    }
}
