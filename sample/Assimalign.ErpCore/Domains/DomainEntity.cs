namespace Assimalign.ErpCore;

using Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract record class DomainEntity<T> : Entity<T>
    where T : Entity<T>
{
    /// <summary>
    /// 
    /// </summary>
    public virtual DomainEntityType? EntityType { get; set; }
}
