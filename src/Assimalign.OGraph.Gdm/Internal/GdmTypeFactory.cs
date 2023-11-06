using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmTypeFactory
{
    private readonly static IDictionary<Type, Func<IOGraphGdmTypeReference>> maps;

    static GdmTypeFactory()
    {
        maps = new Dictionary<Type, Func<IOGraphGdmTypeReference>>()
        {
            { typeof(Byte), () => new GdmTypeReference<GdmByteType>(new GdmByteType()) },
            { typeof(Char), () => new GdmTypeReference<GdmCharType>(new GdmCharType()) },
            { typeof(Char?), () => new GdmTypeReference<GdmNullCharType>(new GdmNullCharType()) },
            { typeof(DateTimeOffset), () => new GdmTypeReference<GdmDateTimeOffsetType>(new GdmDateTimeOffsetType()) },
            { typeof(DateTimeOffset?), () => new GdmTypeReference<GdmNullDateTimeOffsetType>(new GdmNullDateTimeOffsetType()) },
            { typeof(DateTime), () => new GdmTypeReference<GdmDateTimeType>(new GdmDateTimeType()) },
            { typeof(DateTime?), () => new GdmTypeReference<GdmNullDateTimeType>(new GdmNullDateTimeType()) },
            { typeof(DateOnly), () => new GdmTypeReference<GdmDateType>(new GdmDateType()) },
            { typeof(DateOnly?), () => new GdmTypeReference<GdmNullDateType>(new GdmNullDateType()) },
            { typeof(Decimal), () => new GdmTypeReference<GdmDecimalType>(new GdmDecimalType()) },
            { typeof(Decimal?), () => new GdmTypeReference<GdmNullDecimalType>(new GdmNullDecimalType()) },
            { typeof(Double), () => new GdmTypeReference<GdmDoubleType>(new GdmDoubleType()) },
            { typeof(Double?), () => new GdmTypeReference<GdmNullDoubleType>(new GdmNullDoubleType()) },
            { typeof(float), () => new GdmTypeReference<GdmFloatType>(new GdmFloatType()) },
            { typeof(float?), () => new GdmTypeReference<GdmNullFloatType>(new GdmNullFloatType()) },
            { typeof(Guid), () => new GdmTypeReference<GdmGuidType>(new GdmGuidType()) },
            { typeof(Guid?), () => new GdmTypeReference<GdmNullGuidType>(new GdmNullGuidType()) },
            { typeof(Half), () => new GdmTypeReference<GdmHalfType>(new GdmHalfType()) },
            { typeof(Half?), () => new GdmTypeReference<GdmNullHalfType>(new GdmNullHalfType()) },
#if NET7_0_OR_GREATER
            { typeof(Int128), () => new GdmTypeReference<GdmInt128Type>(new GdmInt128Type()) },
            { typeof(Int128?), () => new GdmTypeReference<GdmNullInt128Type>(new GdmNullInt128Type()) },
#endif
            { typeof(Int16), () => new GdmTypeReference<GdmInt16Type>(new GdmInt16Type()) },
            { typeof(Int16?), () => new GdmTypeReference<GdmNullInt16Type>(new GdmNullInt16Type()) },
            { typeof(Int32), () => new GdmTypeReference<GdmInt32Type>(new GdmInt32Type()) },
            { typeof(Int32?), () => new GdmTypeReference<GdmNullInt32Type>(new GdmNullInt32Type()) },
            { typeof(Int64), () => new GdmTypeReference<GdmInt64Type>(new GdmInt64Type()) },
            { typeof(Int64?), () => new GdmTypeReference<GdmNullInt64Type>(new GdmNullInt64Type()) },
            { typeof(string), () => new GdmTypeReference<GdmStringType>(new GdmStringType()) },
            { typeof(TimeSpan), () => new GdmTypeReference<GdmTimeSpanType>(new GdmTimeSpanType()) },
            { typeof(TimeSpan?), () => new GdmTypeReference<GdmNullTimeSpanType>(new GdmNullTimeSpanType()) },
            { typeof(TimeOnly), () => new GdmTypeReference<GdmTimeType>(new GdmTimeType()) },
            { typeof(TimeOnly?), () => new GdmTypeReference<GdmNullTimeType>(new GdmNullTimeType()) },
            { typeof(UInt16), () => new GdmTypeReference<GdmUInt16Type>(new GdmUInt16Type()) },
            { typeof(UInt16?), () => new GdmTypeReference<GdmNullUInt16Type>(new GdmNullUInt16Type()) },
            { typeof(UInt32), () => new GdmTypeReference<GdmUInt32Type>(new GdmUInt32Type()) },
            { typeof(UInt32?), () => new GdmTypeReference<GdmNullUInt32Type>(new GdmNullUInt32Type()) },
            { typeof(UInt64), () => new GdmTypeReference<GdmUInt64Type>(new GdmUInt64Type()) },
            { typeof(UInt64?), () => new GdmTypeReference<GdmNullUInt64Type>(new GdmNullUInt64Type()) },
        };
    }


   
}
