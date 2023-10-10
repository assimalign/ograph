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

    /// <inheritdoc />
    public Name Name => $"{typeof(TEnum).Name}Enum";

    /// <inheritdoc />
    public TypeIdentifier Identifier => TypeIdentifier.Enum;

    /// <inheritdoc />
    public EnumValue[] Values { get; }

    /// <inheritdoc />
    public Type? RuntimeType => typeof(TEnum);

    public bool IsNullable => throw new NotImplementedException();

}