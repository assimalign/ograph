using System;
using System.Xml;
using System.Text.Json;
using System.Linq;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm;

[DebuggerDisplay("Gdm Type ({Kind}): {Label}")]
public class GdmNullEnumType<TEnum> : IOGraphGdmEnumType
    where TEnum : Enum
{
    public GdmNullEnumType()
    {
        Values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>()
            .Select(@enum =>
            {
                var name = Enum.GetName(typeof(TEnum), @enum)!;
                return Enum.GetUnderlyingType(typeof(TEnum)).Name switch
                {
                    nameof(Byte) => new GdmEnumValue(name, Convert.ToByte(@enum)),
                    nameof(SByte) => new GdmEnumValue(name, Convert.ToSByte(@enum)),
                    nameof(Int16) => new GdmEnumValue(name, Convert.ToInt16(@enum)),
                    nameof(UInt16) => new GdmEnumValue(name, Convert.ToUInt16(@enum)),
                    nameof(Int32) => new GdmEnumValue(name, Convert.ToInt32(@enum)),
                    nameof(UInt32) => new GdmEnumValue(name, Convert.ToUInt32(@enum)),
                    nameof(Int64) => new GdmEnumValue(name, Convert.ToInt64(@enum)),
                    nameof(UInt64) => new GdmEnumValue(name, Convert.ToUInt64(@enum)),
                };
            }).ToArray();

        if (Values.Length == 0)
        {
            throw new Exception($"'{typeof(TEnum).Name}' has no values. Enum values cannot be empty.");
        }
    }
    public Label Label => $"{typeof(TEnum).Name}EnumType";
    public GdmTypeKind Kind => GdmTypeKind.Enum;
    public GdmEnumValue[] Values { get; }
    public Type RuntimeType => typeof(TEnum);
    public GdmElementType ElementType => GdmElementType.Type;

    public virtual TEnum? Read(ref Utf8JsonReader reader)
    {
        throw new NotImplementedException();
    }
    public virtual TEnum? Read(XmlReader reader)
    {
        throw new NotImplementedException();
    }
    public virtual void Write(Utf8JsonWriter writer, TEnum value)
    {
        throw new NotImplementedException();
    }
    public virtual void Write(XmlWriter writer, TEnum value)
    {
        throw new NotImplementedException();
    }

    object IOGraphGdmType.Read(ref Utf8JsonReader reader) => Read(ref reader)!;
    object IOGraphGdmType.Read(XmlReader reader) => Read(reader)!;
    void IOGraphGdmType.Write(Utf8JsonWriter writer, object value) => Write(writer, AssertType(value));
    void IOGraphGdmType.Write(XmlWriter writer, object value) => Write(writer, AssertType(value));
    private TEnum AssertType(object value)
    {
        if (value is not TEnum type)
        {
            throw new InvalidOperationException($"Could not write type {value.GetType().Name}. Expected {typeof(TEnum).Name}");
        }
        return type;
    }
}
