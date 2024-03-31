namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmPrimitiveType : IOGraphGdmType
{
    /// <summary>
    /// An array of Regex patterns that are acceptable formats.
    /// </summary>
    string[]? Formats { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    object Parse(string? value);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    bool TryParse(string? value, out object? result);
}