namespace Assimalign.OGraph.Syntax;

/// <summary>
/// The location of the node.
/// </summary>
public readonly struct Location
{
    Location(int startLine, int endLine, int start, int end)
    {
        EndLine = endLine;
        StartLine = startLine;
        Start = start;
        End = end;
    }

    /// <summary>
    /// The start position.
    /// </summary>
    public int Start { get; }
    /// <summary>
    /// The beginning line number of the node.
    /// </summary>
    public int StartLine { get; }    
    /// <summary>
    /// The end position.
    /// </summary>
    public int End { get; }
    /// <summary>
    /// The ending line number of the node.
    /// </summary>
    public int EndLine { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="startLine"></param>
    /// <param name="endLine"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static Location Create(int startLine, int endLine, int start, int end) => 
        new Location(startLine, endLine, start, end);
}