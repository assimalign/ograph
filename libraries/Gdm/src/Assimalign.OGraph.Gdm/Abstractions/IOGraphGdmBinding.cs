using System.Threading;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmBinding
{
    /// <summary>
    /// The binding label.
    /// </summary>
    Label Label { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task ExecuteAsync(IOGraphGdmBindingContext context, CancellationToken cancellationToken = default);
}
