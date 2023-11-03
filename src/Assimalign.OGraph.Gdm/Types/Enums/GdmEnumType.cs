using System;
using System.Linq;
using System.Text.Json;
using System.Xml;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmEnumType<TEnum> : IOGraphGdmEnumType
    where TEnum : struct, Enum
{
    public GdmEnumType()
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

    public Label Label => $"{typeof(TEnum).Name}Enum";
    public GdmTypeKind Kind => GdmTypeKind.Enum;
    public EnumValue[] Values { get; }
    public Type RuntimeType => typeof(TEnum);
    public bool IsAssignableTo(IOGraphGdmType type)
    {
        return RuntimeType!.IsAssignableFrom(type.RuntimeType);
    }

    public object Read(ref Utf8JsonReader reader)
    {
        throw new NotImplementedException();
    }

    public object Read(XmlReader reader)
    {
        throw new NotImplementedException();
    }

    public void Write(Utf8JsonWriter writer, object value)
    {
        throw new NotImplementedException();
    }

    public void Write(XmlWriter writer, object value)
    {
        throw new NotImplementedException();
    }
}