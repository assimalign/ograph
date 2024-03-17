using System;

namespace Assimalign.OGraph.Gdm.Tests;

public readonly struct WorkItemId
{
    public WorkItemId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static implicit operator Guid(WorkItemId employeeId) => employeeId.Value;
    public static implicit operator WorkItemId(Guid value) => new WorkItemId(value);
}