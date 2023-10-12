using System;
using System.Collections.Generic;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphMetadata : IReadOnlyDictionary<string, object>
{
    /// <summary>
    /// Checks whether the value is valid for serialization.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    bool IsValid(object value);
}
