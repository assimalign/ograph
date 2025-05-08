using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public abstract class GdmMetaConvertor
{
    /// <summary>
    /// The name of the provider.
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public abstract Task WriteAsync(XmlWriter writer, object value, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public abstract Task<object> ReadAsync(XmlReader reader, CancellationToken cancellationToken);
}