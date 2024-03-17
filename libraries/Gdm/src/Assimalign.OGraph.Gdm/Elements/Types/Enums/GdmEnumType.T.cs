using System;
using System.Xml;
using System.Text.Json;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Assimalign.OGraph.Gdm;

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
    public override GdmTypeKind Kind => GdmTypeKind.Enum;

    public override unsafe TEnum Read(ref Utf8JsonReader reader)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            return Enum.Parse<TEnum>(reader.GetString()!);
        }
        if (reader.TokenType == JsonTokenType.Number)
        {
            switch (Type.GetTypeCode(RuntimeType))
            {
                case TypeCode.Int32:
                    {
                        if (reader.TryGetInt32(out var value8))
                        {
                            return Unsafe.As<int, TEnum>(ref value8);
                        }
                        break;
                    }
                case TypeCode.UInt32:
                    {
                        if (reader.TryGetUInt32(out var value4))
                        {
                            return Unsafe.As<uint, TEnum>(ref value4);
                        }
                        break;
                    }
                case TypeCode.UInt64:
                    {
                        if (reader.TryGetUInt64(out var value6))
                        {
                            return Unsafe.As<ulong, TEnum>(ref value6);
                        }
                        break;
                    }
                case TypeCode.Int64:
                    {
                        if (reader.TryGetInt64(out var value2))
                        {
                            return Unsafe.As<long, TEnum>(ref value2);
                        }
                        break;
                    }
                case TypeCode.SByte:
                    {
                        if (reader.TryGetSByte(out var value7))
                        {
                            return Unsafe.As<sbyte, TEnum>(ref value7);
                        }
                        break;
                    }
                case TypeCode.Byte:
                    {
                        if (reader.TryGetByte(out var value5))
                        {
                            return Unsafe.As<byte, TEnum>(ref value5);
                        }
                        break;
                    }
                case TypeCode.Int16:
                    {
                        if (reader.TryGetInt16(out var value3))
                        {
                            return Unsafe.As<short, TEnum>(ref value3);
                        }
                        break;
                    }
                case TypeCode.UInt16:
                    {
                        if (reader.TryGetUInt16(out var value))
                        {
                            return Unsafe.As<ushort, TEnum>(ref value);
                        }
                        break;
                    }
            }
        }
        throw new Exception();
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