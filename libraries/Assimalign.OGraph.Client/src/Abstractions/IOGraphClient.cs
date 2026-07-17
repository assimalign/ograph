using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Client;

public interface IOGraphClient
{
    Task<IOGraphClientResponse> GetAsync(IOGraphClientRequest request, CancellationToken cancellationToken = default);
    Task<IOGraphClientResponse> PutAsync(IOGraphClientRequest request, CancellationToken cancellationToken = default);
    Task<IOGraphClientResponse> PostAsync(IOGraphClientRequest request, CancellationToken cancellationToken = default);
    Task<IOGraphClientResponse> PatchAsync(IOGraphClientRequest request, CancellationToken cancellationToken = default);
    Task<IOGraphClientResponse> DeleteAsync(IOGraphClientRequest request, CancellationToken cancellationToken = default);
}