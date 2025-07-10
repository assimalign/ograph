using System;
using System.Collections.Generic;
using System.Text;

namespace Assimalign.OGraph;

public abstract class Entity<T, TKey> : Entity<T> 
    where T : Entity<T, TKey> 
    where TKey : notnull
{

    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public TKey Id { get; set; } = default!; // Default value for TKey, must be set in derived classes.
}
