using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

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
