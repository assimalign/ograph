using System;

namespace Erp;

public readonly struct TaskId
{
    public TaskId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static implicit operator Guid(TaskId employeeId) => employeeId.Value;
    public static implicit operator TaskId(Guid value) => new TaskId(value);
}