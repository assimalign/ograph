using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphNodeProperty
{
    bool IsKey { get; }
    bool IsComputed { get; }
    bool IsPagable { get; }
    bool IsFilterable { get; }

    /// <summary>
    /// The name of the property.
    /// </summary>
    Name PropertyName { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphType PropertyType { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphNodePropertyMetadata Metadata { get; }



    Type? RuntimeType { get; }

    string? RuntimeName { get; }
}
