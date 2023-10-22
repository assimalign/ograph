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

        if (Values.Length == 0)
        {
            throw new Exception($"'{typeof(TEnum).Name}' has no values. Enum values cannot be empty.");
        }
    }

    public Name Name => $"{typeof(TEnum).Name}Enum";
    public TypeKind Kind => TypeKind.Enum;
    public EnumValue[] Values { get; }
    public Type RuntimeType => typeof(TEnum);

    public bool IsNullable => throw new NotImplementedException();

    public bool IsAssignable(IOGraphType type)
    {
        return RuntimeType!.IsAssignableFrom(type.RuntimeType);
    }
}