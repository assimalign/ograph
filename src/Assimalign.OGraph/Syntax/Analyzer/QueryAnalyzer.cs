using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Syntax.Analyzer;

/// <summary>
/// An abstract analyzer for implementing custom analysis on parsed queries.
/// </summary>
public abstract class QueryAnalyzer
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="document"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public abstract Task AnalyzeAsync(QueryDocument document, CancellationToken cancellationToken = default);
}
