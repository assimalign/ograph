using System;
using System.Collections.Generic;

namespace Assimalign.ErpCore.Entities;

public readonly struct EmployeeId : 
    IEquatable<EmployeeId>, 
    IEqualityComparer<EmployeeId>
{
    public EmployeeId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }
    public override string ToString()
    {
        return Value.ToString();
    }
    public override bool Equals(object? instance)
    {
        if (instance is EmployeeId employeeId)
        {
            return Equals(employeeId);
        }
        return false;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(typeof(EmployeeId), Value);
    }
    public bool Equals(EmployeeId other)
    {
        return Value.Equals(other.Value);
    }
    public bool Equals(EmployeeId left, EmployeeId right)
    {
        return left.Equals(right);
    }
    public int GetHashCode(EmployeeId instance)
    {
        return instance.GetHashCode();
    }


    public static implicit operator EmployeeId(Guid value) => new EmployeeId(value);
    public static implicit operator Guid(EmployeeId value) => value.Value;
}
