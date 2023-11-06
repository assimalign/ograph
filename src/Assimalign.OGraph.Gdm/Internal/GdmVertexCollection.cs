using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmVertexCollection : HashSet<IOGraphGdmVertex>,
    IOGraphGdmVertexCollection
{
    public static IEqualityComparer<IOGraphGdmVertex> comparer = new VertexComparer();
    public GdmVertexCollection()
        : base(comparer) { }

    public IOGraphGdmVertex this[Label label]
    {
        get
        {
            if (TryGet(label, out var vertex))
            {
                return vertex;
            }
            throw new KeyNotFoundException();
        }
    }
    public bool IsReadOnly { get; set; }
    public bool TryGet(Label label, out IOGraphGdmVertex? node)
    {
        node = this.FirstOrDefault(vertex => vertex.Label == label);

        return node is null ? false : true;
    }
    public bool TryAdd(IOGraphGdmVertex vertex)
    {
        throw new NotImplementedException();
    }
    public new void Add(IOGraphGdmVertex node)
    {
        AssertReadOnly();
        base.Add(node);
    }
    public new void Remove(IOGraphGdmVertex node)
    {
        AssertReadOnly();
        base.Remove(node);
    }
    private void AssertReadOnly()
    {
        if (IsReadOnly)
        {
            throw new InvalidOperationException("The Collection is ReadOnly.");
        }
    }

    private class VertexComparer : IEqualityComparer<IOGraphGdmVertex>
    {
        public bool Equals(IOGraphGdmVertex? left, IOGraphGdmVertex? right)
        {
            if (left is null)
            {
                throw new ArgumentNullException(nameof(left));
            }
            if (right is null)
            {
                throw new ArgumentNullException(nameof(right));
            }
            return left.Label == right.Label;
        }

        public int GetHashCode([DisallowNull] IOGraphGdmVertex instance)
        {
            return HashCode.Combine(instance.Label, instance.Type);
        }
    }
}