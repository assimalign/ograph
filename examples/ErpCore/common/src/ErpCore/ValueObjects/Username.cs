using Assimalign.OGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpCore;

[GdmScalarType(ScalarUnderlyingType.String, IncludeImplicitOperators = true, IncludeIsValidMethod = true)]
public readonly partial struct Username
{
    public static partial bool IsValid(string value, out string message)
    {
        throw new NotImplementedException();
    }
}
