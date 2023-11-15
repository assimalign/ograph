using System;
using System.Diagnostics.CodeAnalysis;

namespace Assimalign.OGraph.Gdm.Internal;

using Properties;

internal static class GdmThrowHelper
{
    #region Common Exceptions

    [DoesNotReturn]
    public static void ThrowArgumentNullException(string paramName) =>
        throw new ArgumentNullException(paramName);

    #endregion

    [DoesNotReturn]
    public static void ThrowUnknownError() =>
        throw new GdmUnknownException();

    #region Validation/Model Exceptions

    [DoesNotReturn]
    public static void ThrowComplexTypeKeyDisallowed() =>
        throw new GdmModelException(OGraphGdmErrorCode.GDM0301, Resources.GDM0301);

    [DoesNotReturn]
    public static void ThrowComplexTypeKeyDisallowed(string source) =>
        throw new GdmModelException(OGraphGdmErrorCode.GDM0301, source, Resources.GDM0301);

    [DoesNotReturn]
    public static void ThrowVertexInvalidTypeReferenceIsNotEntityType() =>
        throw new GdmModelException(OGraphGdmErrorCode.GDM1001, Resources.GDM1001);

    [DoesNotReturn]
    public static void ThrowVertexInvalidTypeReferenceIsNotEntityType(string source) =>
        throw new GdmModelException(OGraphGdmErrorCode.GDM1001, source, Resources.GDM1001);

    [DoesNotReturn]
    public static void ThrowVertexInvalidTypeReferenceIsNull() =>
        throw new GdmModelException(OGraphGdmErrorCode.GDM1002, Resources.GDM1002);

    [DoesNotReturn]
    public static void ThrowVertexInvalidTypeReferenceIsNull(string source) =>
        throw new GdmModelException(OGraphGdmErrorCode.GDM1002, source, Resources.GDM1002);

    [DoesNotReturn]
    public static void ThrowInvalidLabel(string source) =>
        throw new GdmModelException(OGraphGdmErrorCode.GDM5001, source, Resources.GDM5001);

    #endregion


    #region Serialization Exceptions

    [DoesNotReturn]
    public static void ThrowInvalidContentException(string source) =>
        throw new GdmSerializationException(OGraphGdmErrorCode.GDM3001, source, Resources.GDM3001);


    #endregion

    
}
