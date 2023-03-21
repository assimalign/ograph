using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public readonly struct PathSegment
{

    internal PathSegment(string value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }
        this.Value = value;
    }

    public string Value { get; }
}
