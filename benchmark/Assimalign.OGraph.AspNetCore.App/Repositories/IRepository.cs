using Microsoft.Identity.Client;
using System.Linq;

namespace Assimalign.OGraph;

public interface IRepository
{

    IQueryable Queryable { get; }
}

public interface IRepository<T> : IRepository
{
    IQueryable<T> Queryable { get; }
}
