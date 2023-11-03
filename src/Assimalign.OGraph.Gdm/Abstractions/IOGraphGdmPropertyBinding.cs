using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmPropertyBinding
{
    Task InvokeAsync(IOGraphGdmPropertyBindingContext context, CancellationToken cancellationToken = default);
}
