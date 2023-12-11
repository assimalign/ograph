using Temp.Test;

try
{
    var values = GetEnumValues(typeof(TestEnum));

    foreach (var value in values)
    {
        Console.WriteLine(value.Name);
    }
}
catch (Exception exception)
{
    Console.WriteLine(exception.Message);
}

Console.ReadKey();


static GdmEnumValue[] GetEnumValues(Type runtimeType)
{
    var values = Enum.GetValues(runtimeType);
    var underlyingType = Enum.GetUnderlyingType(runtimeType);

    var index = 0;
    var items = new GdmEnumValue[values.Length];

    foreach (var value in values)
    {
        var name = Enum.GetName(runtimeType, value);
        var item = underlyingType.Name switch
        {
            nameof(Byte) => new GdmEnumValue(name, Convert.ToByte(value)),
            nameof(SByte) => new GdmEnumValue(name, Convert.ToSByte(value)),
            nameof(Int16) => new GdmEnumValue(name, Convert.ToInt16(value)),
            nameof(UInt16) => new GdmEnumValue(name, Convert.ToUInt16(value)),
            nameof(Int32) => new GdmEnumValue(name, Convert.ToInt32(value)),
            nameof(UInt32) => new GdmEnumValue(name, Convert.ToUInt32(value)),
            nameof(Int64) => new GdmEnumValue(name, Convert.ToInt64(value)),
            nameof(UInt64) => new GdmEnumValue(name, Convert.ToUInt64(value)),
            _ => throw new Exception("An unknown exception occurred")
        };

        items[index] = item;
        index++;
    }

    return items;
}