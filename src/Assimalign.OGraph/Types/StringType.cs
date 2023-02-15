namespace Assimalign.OGraph;

public sealed class StringType : IOGraphType
{
    public Label TypeName => "String";

    public IOGraphTypeResolver TypeResolver { get; internal set; }

    public string? TypeDescription => throw new System.NotImplementedException();

    public OGraphTypeIdentifier TypeIdentifier => throw new System.NotImplementedException();
}
