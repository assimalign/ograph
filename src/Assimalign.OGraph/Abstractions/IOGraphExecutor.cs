using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphExecutor
{
    Task<IOGraphExecutorResponse> ExecuteAsync(IOGraphExecutorRequest request, CancellationToken cancellationToken = default);
}

public interface IOGraphExecutorContext
{
    /// <summary>
    /// 
    /// </summary>
    IOGraphExecutorRequest Request { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphExecutorResponse Response { get; }
    /// <summary>
    /// 
    /// </summary>
    IServiceProvider ServiceProvider { get; }
}

public interface IOGraphExecutorRequest
{
    /// <summary>
    /// 
    /// </summary>
    Path Path { get; }
    /// <summary>
    /// 
    /// </summary>
    Method Method { get; }
    /// <summary>
    /// 
    /// </summary>
    Stream Body { get; }


}
public interface IOGraphExecutorResponse
{

}