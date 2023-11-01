using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Internal;

internal class OGraphOperationCollection : HashSet<IOGraphOperation>,
    IOGraphOperationCollection
{
    public OGraphOperationCollection() : base()
    {
        
    }
    public bool TryGetOperation(Label name, out IOGraphOperation operation)
    {
        throw new NotImplementedException();
    }



    private class OGraphOperationComparer : IEqualityComparer<IOGraphOperation>
    {
        public bool Equals(IOGraphOperation? left, IOGraphOperation? right)
        {
            if (left is null && right is null)
            {
                throw new ArgumentNullException(nameof(left));
            }
            if ((left is null && right is not null) || (left is not null && right is null))
            {
                return false;
            }

            return left.Label.Equals(right.Label);
        }

        public int GetHashCode([DisallowNull] IOGraphOperation obj)
        {
            throw new NotImplementedException();
        }
    }
}
