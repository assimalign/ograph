namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TNode"></typeparam>
public abstract class OGraphOperation<TNode> : OGraphOperation
    where TNode : IOGraphNode, new()
{
    public OGraphOperation()
    {
        this.node = new TNode();
    }
}
