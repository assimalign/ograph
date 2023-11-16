namespace Assimalign.ErpCore;

using Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract record class DomainEntity<T> : Entity<T>
    where T : Entity<T>
{
    public virtual DomainEntityType? EntityType { get; set; }
    public AuditField? Created { get; set; }
    public AuditField? Updated { get; set; }
}
