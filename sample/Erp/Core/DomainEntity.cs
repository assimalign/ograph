using Assimalign.OGraph;

namespace Erp;

public abstract class DomainEntity<T, TKey> : Entity<T> 
    where T : Entity<T>
    where TKey : struct
{
    public virtual new TKey Id { get; set; }
    public abstract Domain Domain { get; }
    public abstract DomainEntityType EntityType { get; }
}
