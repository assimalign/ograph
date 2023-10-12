using System;
using System.Linq;

namespace Assimalign.OGraph;

public sealed class EnumType<TEnum> : IOGraphEnumType
    where TEnum : struct, Enum
{
    public EnumType()
    {
        Values = Enum.GetValues<TEnum>().Select(value =>
            new EnumValue(
                Enum.GetName<TEnum>(value)!,
                value)).ToArray();
    }

    public Name Name => $"{typeof(TEnum).Name}Enum";
    public TypeIdentifier Identifier => TypeIdentifier.Enum;
    public EnumValue[] Values { get; }
    public Type RuntimeType => typeof(TEnum);

    public bool IsNullable => throw new NotImplementedException();

    public bool IsAssignable(object value)
    {
        return RuntimeType.IsAssignableFrom(value.GetType());
    }
}