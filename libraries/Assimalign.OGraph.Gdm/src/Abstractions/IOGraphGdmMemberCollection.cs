using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmMemberCollection : IEnumerable<IOGraphGdmMember>
{
    /// <summary>
    /// 
    /// </summary>
    int Count { get; }

    /// <summary>
    /// 
    /// </summary>
    bool IsReadOnly { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphGdmMember this[GdmName name] { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="member"></param>
    void Add(IOGraphGdmMember member);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="member"></param>
    void Remove(IOGraphGdmMember member);

    /// <summary>
    /// 
    /// </summary>
    void Clear();
}
