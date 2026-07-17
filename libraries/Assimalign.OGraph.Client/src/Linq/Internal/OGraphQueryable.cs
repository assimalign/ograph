using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Client.Linq.Internal;

internal class OGraphQueryable<T> : IQueryable<T>
{
    public Type ElementType => throw new NotImplementedException();

    public Expression Expression => throw new NotImplementedException();

    public IQueryProvider Provider => throw new NotImplementedException();

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}
