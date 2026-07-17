using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class QueryProvider<T> : QueryProvider 
{
    public QueryProvider()
    {
        ElementType = typeof(T);
    }
}
