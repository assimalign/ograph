namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmScalarType : IOGraphGdmSerializableType
{
    /// <summary>
    /// An array of Regex patterns that are acceptable formats.
    /// </summary>
    string?[]? Formats { get; }

    /// <summary>
    /// The primitive type the scalar type represents.
    /// </summary>
    GdmPrimitiveType PrimitiveType { get; }

    /// <summary>
    /// Parses the value type.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    object Parse(object? value);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    bool TryParse(object? value, out object? result);
}