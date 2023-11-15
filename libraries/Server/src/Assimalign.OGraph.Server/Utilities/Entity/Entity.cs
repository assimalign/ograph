using System;
using System.Collections.Generic;

namespace Assimalign.OGraph;

public abstract partial record class Entity<T> 
    where T : Entity<T>
{
    /// <summary>
    /// The unique identifier for the Entity.
    /// </summary>
    public virtual object? Id { get; set; }
    /// <summary>
    /// This includes meta data regarding the entity.
    /// </summary>
    public IDictionary<string, string> Meta { get; set; } = new Dictionary<string, string>();
}