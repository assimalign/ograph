using System;
using System.Xml;
using System.Text.Json;
using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm.Elements;

using Internal;

public sealed class GdmEnumType<T> : GdmEnumType where T : struct, Enum
{
    #region Constructors

    internal GdmEnumType()
    {
        Values = GetEnumValues();
    }
    internal GdmEnumType(GdmName name, GdmGraph graph) : base(name, graph)
    {
        Values = GetEnumValues();
    }
    internal GdmEnumType(GdmGraph graph) : this(typeof(T).Name, graph) { }

    #endregion

    #region Properties

    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
    public sealed override Type RuntimeType { get; } = typeof(T);
    public sealed override GdmEnumValue[] Values { get; }

    #endregion

    #region Methods

    public new unsafe T Read(ref Utf8JsonReader reader)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            return Enum.Parse<T>(reader.GetString()!);
        }
        if (reader.TokenType == JsonTokenType.Number)
        {
            switch (Type.GetTypeCode(RuntimeType))
            {
                case TypeCode.Int32:
                    {
                        if (reader.TryGetInt32(out var value8))
                        {
                            return Unsafe.As<int, T>(ref value8);
                        }
                        break;
                    }
                case TypeCode.UInt32:
                    {
                        if (reader.TryGetUInt32(out var value4))
                        {
                            return Unsafe.As<uint, T>(ref value4);
                        }
                        break;
                    }
                case TypeCode.UInt64:
                    {
                        if (reader.TryGetUInt64(out var value6))
                        {
                            return Unsafe.As<ulong, T>(ref value6);
                        }
                        break;
                    }
                case TypeCode.Int64:
                    {
                        if (reader.TryGetInt64(out var value2))
                        {
                            return Unsafe.As<long, T>(ref value2);
                        }
                        break;
                    }
                case TypeCode.SByte:
                    {
                        if (reader.TryGetSByte(out var value7))
                        {
                            return Unsafe.As<sbyte, T>(ref value7);
                        }
                        break;
                    }
                case TypeCode.Byte:
                    {
                        if (reader.TryGetByte(out var value5))
                        {
                            return Unsafe.As<byte, T>(ref value5);
                        }
                        break;
                    }
                case TypeCode.Int16:
                    {
                        if (reader.TryGetInt16(out var value3))
                        {
                            return Unsafe.As<short, T>(ref value3);
                        }
                        break;
                    }
                case TypeCode.UInt16:
                    {
                        if (reader.TryGetUInt16(out var value))
                        {
                            return Unsafe.As<ushort, T>(ref value);
                        }
                        break;
                    }
            }
        }
        throw new Exception();
    }
    public new unsafe T Read(XmlReader reader)
    {
        return default;
    }
    public void Write(Utf8JsonWriter writer, T value)
    {

    }
    public void Write(XmlWriter writer, T value)
    {

    }
    public sealed override void Write(Utf8JsonWriter writer, object value)
    {
        Write(writer, ThrowHelper.ThrowIfNotType<T>(value));
    }
    public sealed override void Write(XmlWriter writer, object value)
    {
        Write(writer, ThrowHelper.ThrowIfNotType<T>(value));
    }
    private GdmEnumValue[] GetEnumValues()
    {
        var enumValues = Enum.GetValues<T>();
        var enumConverter = GetConverter();

        if (enumValues is null || enumValues.Length == 0)
        {
            throw new Exception();
        }

        var values = new GdmEnumValue[enumValues.Length];

        for (int i = 0; i < enumValues.Length; i++)
        {
            values[i] = enumConverter(
                Enum.GetName<T>(enumValues[i])!,
                enumValues[i]);
        }

        return values;
    }
    private Func<string, object, GdmEnumValue> GetConverter() => Enum.GetUnderlyingType(typeof(T)).Name switch
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

    #endregion
}