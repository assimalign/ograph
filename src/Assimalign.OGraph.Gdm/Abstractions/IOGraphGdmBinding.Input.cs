using System;
using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmInputBinding : IOGraphGdmBinding
{
    Task InvokeAsync(object context, CancellationToken cancellationToken = default);
}
