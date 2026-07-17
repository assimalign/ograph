using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Assimalign.OGraph;

using Internal;

public abstract partial class Entity<T> : Entity where T : Entity<T>
{
    private readonly Type _type;

    private ConcurrentBag<EntityChange>? _changes;
    private bool _isTracking;

    public Entity()
    {
        _type = typeof(T);
    }



    /// <summary>
    /// Begins tracking property entries.
    /// </summary>
    public void BeginTracking()
    {
        _changes = _type.CaptureEntityProperties(() => this);


        foreach (var state in _changes)
        {
            state.SetOriginal();
        }

        _isTracking = true;
    }

    /// <summary>
    /// Ends property entry tracking.
    /// </summary>
    public void EndTracking()
    {
        if (!_isTracking)
        {
            throw new InvalidOperationException("BeginTracking was not called.");
        }

        foreach (var state in _changes!)
        {
            state.SetCurrent();
        }

        _isTracking = false;
    }

    /// <summary>
    /// 
    /// </summary>
    public bool HasChanged<TProperty>(Expression<Func<T, TProperty>> expression, out EntityChange? change)
    {
        change = null;

        if (_changes is null || _changes.IsEmpty)
        {
            return false;
        }

        if (expression.Body is MemberExpression member)
        {
            var path = string.Join('.', member.ToString().Split('.').Skip(1));

            if ((change = _changes.FirstOrDefault(p => p.PropertyPath == path && p.Kind != EntityChangeKind.None)) is not null)
            {
                return true;
            }

            return false;
        }

        throw new ArgumentException("The provided expression must be a Member Expression");
    }


    public IEnumerable<EntityChange> GetChanges()
    {
        return (_changes ?? []).Where(p => p.Kind != EntityChangeKind.None);
    }
}