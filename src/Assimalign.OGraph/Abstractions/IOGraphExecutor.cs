using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphExecutor
{
    Task<IOGraphExecutorResponse> ExecuteAsync(IOGraphExecutorRequest request, CancellationToken cancellationToken = default);
}


public interface IOGraphExecutorContext
{
    IOGraphExecutorRequest Request { get; }
    IOGraphExecutorResponse Response { get; }
}

public interface IOGraphExecutorRequest
{

}
public interface IOGraphExecutorResponse
{

}