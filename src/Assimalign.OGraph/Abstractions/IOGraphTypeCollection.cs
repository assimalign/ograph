using System.Collections.Generic;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphTypeCollection : ICollection<IOGraphType>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IOGraphType this[Label name] { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    bool TryAddType(IOGraphType type);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    bool TryGetType(Label name, out IOGraphType? type);
}