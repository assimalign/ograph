using System;

namespace ErpCore;

using Assimalign.OGraph;

[GdmScalarType(ScalarUnderlyingType.Int, 
    IncludeImplicitOperators = true,
    IncludeIsValidMethod = true,
    GdmTypeNamespace = "ErpCore.Gdm")]
[GdmOmittedComplexType("TestValue")]
public partial class UserId
{
    public static partial bool IsValid(int value, out string message)
    {
        message = string.Empty;

        return false;
    }
}
