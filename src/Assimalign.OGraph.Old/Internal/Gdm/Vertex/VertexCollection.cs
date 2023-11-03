using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Internal;

internal class VertexCollection : HashSet<IOGraphVertex>,
    IOGraphVertexCollection
{
    public static IEqualityComparer<IOGraphVertex> comparer = new VertexComparer();
    public VertexCollection()
        : base(comparer) { }


    public bool IsReadOnly { get; set; }
    public bool TryGetVertex(Label label, out IOGraphVertex? node)
    {
        node = this.FirstOrDefault(vertex => vertex.Labels.Contains(label));

        return node is null ? false : true;
    }

    public new void Add(IOGraphVertex node)
    {
        AssertReadOnly();
        base.Add(node);
    }
    public new void Remove(IOGraphVertex node)
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


    private class VertexComparer : IEqualityComparer<IOGraphVertex>
    {
        public bool Equals(IOGraphVertex? left, IOGraphVertex? right)
        {
            if (left is null)
            {
                throw new ArgumentNullException(nameof(left));
            }
            if (right is null)
            {
                throw new ArgumentNullException(nameof(right));
            }
            return left.Labels.Except(right.Labels).Count() == 0;
        }

        public int GetHashCode([DisallowNull] IOGraphVertex instance)
        {
            var hashCode = new HashCode();

            foreach (var label in instance.Labels)
            {
                hashCode.Add(label);
            }

            return hashCode.ToHashCode();
        }
    }
}