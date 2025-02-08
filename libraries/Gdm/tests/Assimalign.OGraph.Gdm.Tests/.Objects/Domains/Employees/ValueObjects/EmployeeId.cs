using System;

namespace Assimalign.OGraph.Gdm.Tests.Objects;

public readonly struct EmployeeId
{
    public EmployeeId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static implicit operator Guid(EmployeeId employeeId) => employeeId.Value;
    public static implicit operator EmployeeId(Guid value )=> new EmployeeId(value);
}
