namespace Assimalign.OGraph;

public sealed class StringType : IOGraphType
{
    public Name TypeName => "String";
    public OGraphTypeIdentifier TypeIdentifier => OGraphTypeIdentifier.Primitive;
    public IOGraphTypeResolver TypeResolver { get; internal set; }
}
