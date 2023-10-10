namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public abstract class OGraphQueryOptions
{
    /// <summary>
    /// Enables or disables sorting. Default is true;
    /// </summary>
    public bool CanSort { get; set; } = true;
    /// <summary>
    /// Enables or disables filtering. Default is true.
    /// </summary>
    public bool CanFilter { get; set; } = true;
    /// <summary>
    /// Enables or disables paging. Default is true.
    /// </summary>
    public bool CanPage { get; set; } = true;
    /// <summary>
    /// Enables or disables projections. Default is true.
    /// </summary>
    public bool CanProject { get; set; } = true;
    /// <summary>
    /// The max amount of nodes a user is allowed to retreive.
    /// </summary>
    public int? MaxPageSize { get; set; } 
    /// <summary>
    /// 
    /// </summary>
    public int? DefaultPageSize { get; set; } = 100;
    /// <summary>
    /// Specifies whether to select all properties of no projections are supplied.
    /// </summary>
    public bool DefaultProjectAll { get; set; }
}
