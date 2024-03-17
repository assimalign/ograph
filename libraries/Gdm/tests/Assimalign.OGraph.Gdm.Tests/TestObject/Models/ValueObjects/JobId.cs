using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests;

public readonly struct JobId
{
    public JobId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static implicit operator Guid(JobId employeeId) => employeeId.Value;
    public static implicit operator JobId(Guid value) => new JobId(value);
}