namespace Assimalign.OGraph;

public abstract partial class Entity<T> 
    where T : Entity<T>
{
    /// <summary>
    /// The unique identifier for the Entity.
    /// </summary>
    public virtual object? Id { get; set; }
}