using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Assimalign.OGraph;

[DebuggerDisplay("{ToString()}")]
public class EntityChange
{
    private static readonly (
        Func<object?, object?, bool> Predicate,
        EntityChangeKind State
    )[] _states = [
        ((o, c) => o is null && c is null, EntityChangeKind.None),
        ((o, c) => o is null && c is not null, EntityChangeKind.Added),
        ((o, c) => o is not null && c is null, EntityChangeKind.Removed),
        ((o, c) => o is IComparable comp && c is IComparable && comp.CompareTo(c) != 0, EntityChangeKind.Modified),
        ((o, c) => !o!.Equals(c), EntityChangeKind.Modified)
    ];

    private readonly Type _propertyType;
    private readonly Func<object?> _propertyParentGetter;
    private readonly Func<object?, object?> _propertyGetter;
    private readonly string _propertyName;
    private readonly string _propertyPath;
    private readonly IReadOnlyList<EntityChange> _childChanges = [];

    private object? _original;
    private object? _current;
    private EntityChangeKind _kind;

    internal EntityChange(
        string propertyName,
        string propertyPath,
        Func<object?> propertyParentGetter, // Should return the parent/declaring type of the property
        Func<object?, object?> propertyGetter,
        Type propertyType)
    {
        _propertyType = propertyType;
        _propertyName = propertyName;
        _propertyPath = propertyPath;
        _propertyGetter = propertyGetter;
        _propertyParentGetter = propertyParentGetter;
    }

    internal EntityChange(
        string propertyName,
        string propertyPath,
        Func<object?> propertyParentGetter, // Should return the parent/declaring type of the property
        Func<object?, object?> propertyGetter,
        Type propertyType, 
        IReadOnlyList<EntityChange> nested) : this(propertyName, propertyPath, propertyParentGetter, propertyGetter, propertyType)
    {
        _childChanges = nested;
    }

    /// <summary>
    /// The name of the property that changed.
    /// </summary>
    public string PropertyName => _propertyName;

    /// <summary>
    /// The property path, if any.
    /// </summary>
    public string PropertyPath => _propertyPath;

    /// <summary>
    /// The original value of the property at the beginning of change tracking.
    /// </summary>
    public object? Original => _original;

    /// <summary>
    /// The new value, if any, of the current property after end tracking.
    /// </summary>
    public object? Current => _current;

    /// <summary>
    /// The state of the change, if any.
    /// </summary>
    public EntityChangeKind Kind => _kind;

    /// <summary>
    /// Gets the child changes, if any.
    /// </summary>
    public IReadOnlyList<EntityChange> ChildChanges => _childChanges.Where(p=>p.Kind != EntityChangeKind.None).ToImmutableList();


    public override string ToString()
    {
        if (_childChanges.Count > 0)
        {
            return $"[{Kind}] {_propertyName}";
        }

        return $"[{Kind}] {_propertyName} = {_original} -> {_current}";
    }

    internal virtual void SetOriginal()
    {
        object? parent = _propertyParentGetter.Invoke();

        if (parent is not null)
        {
            _original = _propertyGetter.Invoke(parent);
        }

        for (int i = 0; i < _childChanges.Count; i++)
        {
            _childChanges[i].SetOriginal();
        }
    }

    internal virtual void SetCurrent()
    {
        object? parent = _propertyParentGetter.Invoke();

        if (parent is not null)
        {
            _current = _propertyGetter.Invoke(parent);
        }

        for (int i = 0; i < _states.Length; i++)
        {
            var check = _states[i];

            if (check.Predicate.Invoke(_original, _current))
            {
                _kind = check.State;
                break;
            }
        }

        for (int i = 0; i < _childChanges.Count; i++)
        {
            _childChanges[i].SetCurrent();
        }
    }
}
