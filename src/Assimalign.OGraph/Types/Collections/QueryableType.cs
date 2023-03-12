using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public sealed class QueryableType<T> : CollectionType<IQueryable<T>>
    where T : class
{



    public IQueryable<T> Queryable { get; init; }

    public override IOGraphType ItemType => throw new NotImplementedException();
}
