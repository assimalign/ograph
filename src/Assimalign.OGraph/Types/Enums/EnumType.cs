using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Assimalign.OGraph;

public sealed class EnumType<TEnum> : IOGraphEnumType
    where TEnum : Enum
{
    public EnumType()
    {
        
    }
    public Name TypeName => $"{typeof(TEnum).Name}Enum";

    /// <inheritdoc />
    public OGraphTypeIdentifier TypeIdentifier => OGraphTypeIdentifier.Enum;
    /// <inheritdoc />
    public Type? RuntimeType => typeof(TEnum);

    public bool IsNullable => throw new NotImplementedException();

    Name IOGraphType.TypeName => throw new NotImplementedException();

    OGraphTypeIdentifier IOGraphType.TypeIdentifier => throw new NotImplementedException();
}