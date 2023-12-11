using System;
using System.Xml;
using System.Text.Json;
using System.Diagnostics;

namespace Assimalign.OGraph.Gdm;

using Assimalign.OGraph.Gdm.Internal;

[DebuggerDisplay("Gdm Type ({Kind}): {Label}")]
public class GdmEnumType<TEnum> : GdmType<TEnum>
    where TEnum : struct, Enum
{
    public GdmEnumType()
    {
        Values = GetEnumValues();
    }

    private GdmEnumValue[] GetEnumValues()
    {
        var enumValues = Enum.GetValues<TEnum>();
        var enumConverter = GetConverter();

        if (enumValues is null || enumValues.Length == 0)
        {
            throw new Exception();
        }

        var values = new GdmEnumValue[enumValues.Length];

        for (int i = 0; i < enumValues.Length; i++)
        {
            values[i] = enumConverter(
                Enum.GetName<TEnum>(enumValues[i])!,
                enumValues[i]);
        }

        return values;
    }

    private Func<string, object, GdmEnumValue> GetConverter() => Enum.GetUnderlyingType(typeof(TEnum)).Name switch
    {
        nameof(Byte) => (string name, object value) => GdmEnumValue.Create(name, Convert.ToByte(value)),
        nameof(SByte) => (string name, object value) => GdmEnumValue.Create(name, Convert.ToSByte(value)),
        nameof(Int16) => (string name, object value) => GdmEnumValue.Create(name, Convert.ToInt16(value)),
        nameof(UInt16) => (string name, object value) => GdmEnumValue.Create(name, Convert.ToUInt16(value)),
        nameof(Int32) => (string name, object value) => GdmEnumValue.Create(name, Convert.ToInt32(value)),
        nameof(UInt32) => (string name, object value) => GdmEnumValue.Create(name, Convert.ToUInt32(value)),
        nameof(Int64) => (string name, object value) => GdmEnumValue.Create(name, Convert.ToInt64(value)),
        nameof(UInt64) => (string name, object value) => GdmEnumValue.Create(name, Convert.ToUInt64(value)),
        _ => throw new Exception()
    };


    public GdmEnumValue[] Values { get; }

    public override Label Label => $"{typeof(TEnum).Name}Enum";
    public override GdmTypeKind Kind => GdmTypeKind.Enum;


    public override TEnum Read(ref Utf8JsonReader reader)
    {
        throw new NotImplementedException();
    }
    public override TEnum Read(XmlReader reader)
    {
        throw new NotImplementedException();
    }
    public override void Write(Utf8JsonWriter writer, TEnum value)
    {
        throw new NotImplementedException();
    }
    public override void Write(XmlWriter writer, TEnum value)
    {
        throw new NotImplementedException();
    }



    private TEnum AssertType(object value)
    {
        if (value is not TEnum type)
        {
            throw new InvalidOperationException($"Could not write type {value.GetType().Name}. Expected {typeof(TEnum).Name}");
        }
        return type;
    }
}