using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Assimalign.OGraph;

public abstract partial class Entity<T>
{
    // This will specify whether to start tracking changes.
    private bool isTracking;

    // Create a concurrent cache to maintain all the changes
    // Want to prevent duplicates so rather than maintain a collection we need  to merge 
    // the changes.
    private readonly ConcurrentDictionary<string, EntityChange> changes = new(StringComparer.InvariantCultureIgnoreCase);


    private readonly static ConcurrentDictionary<Type, PropertyInfo[]> reflectionCache = new();

    /// <summary>
    /// Starts change tracking of the entity. Clears out any existing changes.
    /// </summary>
    public void BeginTracking()
    {
        changes.Clear();
        isTracking = true;
    }

    /// <summary>
    /// Ends change tracking of 
    /// </summary>
    public void EndTracking()
    {
        isTracking = false;
    }

    /// <summary>
    /// Returns all the changes that have been tracked.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<EntityChange> GetChanges()
    {
        // Let's only returned changes that have changed
        return changes.Values.Where(change => change.HasChanged).ToImmutableArray();
    }

    /// <summary>
    /// Checks if a particular property had changed and returns the event arguments
    /// </summary>
    /// <param name="propertyName"></param>
    /// <param name="change"></param>
    /// <returns></returns>
    public bool HasChanged(string propertyName, out EntityChange? change)
    {
        change = null;
        if (changes.TryGetValue(propertyName, out var value) && value.HasChanged)
        {
            change = value;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Uses a member expression tp check of the member had changed.
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="expression"></param>
    /// <param name="change"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public bool HasChanged<TValue>(Expression<Func<T, TValue>> expression, out EntityChange? change)
    {
        change = null;
        if (expression.Body is MemberExpression member)
        {
            var propertyName = string.Join('.', member.ToString().Split('.').Skip(1));
            if (changes.TryGetValue(propertyName, out var value) && value.HasChanged)
            {
                change = value;
                return true;
            }
            return false;
        }
        else
        {
            throw new ArgumentException("The provided expression must be a Member Expression");
        }
    }

    /// <summary>
    /// Begins tracing all property changes
    /// </summary>
    /// <remarks>
    /// <i>Clears out any existing property changes.</i>
    /// </remarks>
    protected void BeginPropertyChange([CallerMemberName] string propertyName = "")
    {
        propertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

        if (!isTracking) return;

        var propertyInfo = reflectionCache
            .GetOrAdd(typeof(T), type => type.GetProperties())
            .First(info => info.Name.Equals(propertyName));

        changes[propertyName] = new EntityChange()
        {
            PropertyName = propertyName,
            Original = propertyInfo.GetValue(this)
        };
    }

    /// <summary>
    /// 
    /// </summary>
    protected void EndPropertyChange([CallerMemberName] string propertyName = "")
    {
        propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        if (!isTracking) return;

        var propertyInfo = reflectionCache
            .GetOrAdd(typeof(T), type => type.GetProperties())
            .First(info => info.Name.Equals(propertyName));

        var propertyType = propertyInfo.PropertyType;
        var propertyTypeArgs = propertyType.GetGenericArguments();

        if (!changes.TryGetValue(propertyName, out var entityChange))
        {
            throw new InvalidOperationException($"No Entity Change was found for property: {propertyName}. Make sure to call BeginPropertyChange before updating the value.");
        }

        entityChange.Current = propertyInfo.GetValue(this);

        var original = entityChange.Original;
        var current = entityChange.Current;

        // Nullable<> types
        if (propertyType.IsValueType && propertyTypeArgs.Length == 1 && propertyType.IsAssignableTo(typeof(Nullable<>).MakeGenericType(propertyTypeArgs[0])))
        {
            // No change
            if (original is null && current is null)
            {
                return;
            }
            // Added
            else if (original is null && current is not null)
            {
                entityChange.ChangeType = EntityChangeType.Added;
            }
            // Removed
            else if (original is not null && current is null)
            {
                entityChange.ChangeType = EntityChangeType.Removed;
            }
            // Updated
            else if (!Nullable.Equals(original, current))
            {
                entityChange.ChangeType = EntityChangeType.Updated;
            }
            // No change
            else
            {
                return;
            }
        }
        else if (propertyType.IsValueType)
        {
            // No change has occurred
            if (original.Equals(current))
            {
                return;
            }
            // Value Types always have a value so 
            // there should only ever be an update
            else if (!original.Equals(current))
            {
                entityChange.ChangeType = EntityChangeType.Updated;
            }
            else
            {
                return;
            }
        }
        // Default to reference equality
        else
        {
            // No change
            if (original is null && current is null)
            {
                return;
            }
            // Added
            else if (original is null && current is not null)
            {
                entityChange.ChangeType = EntityChangeType.Added;
            }
            // Removed
            else if (original is not null && current is null)
            {
                entityChange.ChangeType = EntityChangeType.Removed;
            }
            // Updated
            else if (!original.Equals(current))
            {
                entityChange.ChangeType = EntityChangeType.Updated;
            }
            // No change
            else
            {
                return;
            }
        }


        entityChange.HasChanged = true;
    }
}