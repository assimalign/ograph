using System;

namespace Assimalign.OGraph.Internal;

internal class OGraphNodeProperty : IOGraphNodeProperty
{
    public bool IsKey { get; internal set; }
    public bool IsComputed { get; internal set; }
    public bool IsPagable { get; internal set; }
    public bool IsFilterable { get; internal set; }
    public Name PropertyName { get; internal set; }
    public IOGraphType? PropertyType { get; internal set; }
}
