using System;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm.Internal;

using Properties;

internal static class ThrowHelper
{
    #region Common Exceptions

    public static void ThrowIfNull(object value, string paramName)
    {
        if (value is null)
        {
            ThrowArgumentNullException(paramName);
        }
    }

    public static T ThrowIfNull<T>(T value, string paramName)
    {
        if (value is null)
        {
            ThrowArgumentNullException(paramName);
        }

        return value;
    }

    public static T ThrowIfNotType<T>(object value)
    {
        if (value is not T type)
        {
            throw new ArgumentException("");
        }

        return type;
    }

    [DoesNotReturn]
    public static void ThrowArgumentNullException(string paramName) =>
        throw new ArgumentNullException(paramName);

    [DoesNotReturn]
    public static void ThrowArgumentException(string message) => 
        throw new ArgumentException(message);

    [DoesNotReturn]
    public static void ThrowInvalidOperationException(string message) =>
        throw new InvalidOperationException(message);

    #endregion

    [DoesNotReturn]
    public static void ThrowUnknownError() =>
        throw new GdmUnknownException();

    

    #region Validation/Model Exceptions

    [DoesNotReturn]
    public static void ThrowComplexTypeKeyDisallowed() =>
        throw new GdmModelException(GdmErrorCode.GDM0301, Resources.GDM0301);

    [DoesNotReturn]
    public static void ThrowComplexTypeKeyDisallowed(string source) =>
        throw new GdmModelException(GdmErrorCode.GDM0301, source, Resources.GDM0301);

    [DoesNotReturn]
    public static void ThrowVertexInvalidTypeReferenceIsNotEntityType() =>
        throw new GdmModelException(GdmErrorCode.GDM1001, Resources.GDM1001);

    [DoesNotReturn]
    public static void ThrowVertexInvalidTypeReferenceIsNotEntityType(string source) =>
        throw new GdmModelException(GdmErrorCode.GDM1001, source, Resources.GDM1001);

    [DoesNotReturn]
    public static void ThrowVertexInvalidTypeReferenceIsNull() =>
        throw new GdmModelException(GdmErrorCode.GDM1002, Resources.GDM1002);

    [DoesNotReturn]
    public static void ThrowVertexInvalidTypeReferenceIsNull(string source) =>
        throw new GdmModelException(GdmErrorCode.GDM1002, source, Resources.GDM1002);

    [DoesNotReturn]
    public static void ThrowInvalidLabel(string source) =>
        throw new GdmModelException(GdmErrorCode.GDM5001, source, Resources.GDM5001);

    [DoesNotReturn]
    public static void ThrowInvalidName(string source) =>
        throw new GdmModelException(GdmErrorCode.GDM5001, source, Resources.GDM5001);

    #endregion

    #region Serialization Exceptions

    [DoesNotReturn]
    public static void ThrowInvalidDeserializationContentException(string source, Exception innerException) =>
        throw new GdmSerializationException(GdmErrorCode.GDM3001, Resources.GDM3001, innerException)
        {
            Source = source
        };

    [DoesNotReturn]
    public static void ThrowInvalidDeserializationContentException(string source) =>
        throw new GdmSerializationException(GdmErrorCode.GDM3001, Resources.GDM3001)
        {
            Source = source
        };

    [DoesNotReturn]
    public static void ThrowInvalidSerializationTypeException(Type expected, Type received) =>
        throw new InvalidOperationException($"Invalid type serialization. Expected type {expected.Name}. Received type {received.Name}");
    #endregion
}
