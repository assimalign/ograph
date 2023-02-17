namespace Assimalign.OGraph;

/// <summary>
/// Types represent primitive or complex structure that can be 
/// used to define a property, inputs, and outputs of operations within the graph.
/// </summary>
public interface IOGraphType
{
    /// <summary>
    /// The name of the type.
    /// </summary>
    Name TypeName { get; }
    /// <summary>
    /// 
    /// </summary>
    OGraphTypeIdentifier TypeIdentifier { get; }
    /// <summary>
    /// 
    /// </summary>
    IOGraphTypeResolver TypeResolver { get; }


    bool TrySerialize<TSerizlier>(TSerizlier serializer, out object? result);

    bool TryDeserialze<TSerializer>(TSerializer serializer, out object? result);
}



