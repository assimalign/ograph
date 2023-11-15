using System;
using System.Collections.Generic;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphQueryResult : IOGraphResult
{
    /// <summary>
    /// 
    /// </summary>
    long Count { get; }
    /// <summary>
    /// The query error the occurred.
    /// </summary>
    IOGraphError Error { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphQueryData Data { get; }
}


/// <summary>
/// 
/// </summary>
public interface IOGraphQueryData
{
    /// <summary>
    /// 
    /// </summary>
    IOGraphQueryEdgeData Edges { get; }
}
/// <summary>
/// 
/// </summary>
public interface IOGraphQueryObjectData : 
    IOGraphQueryData,
    ICollection<KeyValuePair<Label, IOGraphQueryDataItem>>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphQueryDataItem this[Label label] { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <param name="item"></param>
    void Add(Label label, IOGraphQueryDataItem item);
}
/// <summary>
/// 
/// </summary>
public interface IOGraphQueryCollectionData : 
    IOGraphQueryData,
    ICollection<IOGraphQueryObjectData>
{

}
/// <summary>
/// 
/// </summary>
public interface IOGraphQueryEdgeData : ICollection<KeyValuePair<Label, IOGraphQueryResult>>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <param name="result"></param>
    void Add(Label label, IOGraphQueryResult result);
}


public interface IOGraphQueryDataItem
{
    object Value { get; }
}
public interface IOGraphQueryDataObjectItem : IOGraphQueryDataItem
{
    new IEnumerable<KeyValuePair<Label, IOGraphQueryDataItem>> Value { get; }
}
public interface IOGraphQueryDataCollectionValue : IOGraphQueryDataItem
{
    new IEnumerable<IOGraphQueryDataItem> Value { get;}
}