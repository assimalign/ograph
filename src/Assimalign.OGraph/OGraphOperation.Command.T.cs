namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TNode"></typeparam>
public abstract class OGraphCommandOperation<TNode> : OGraphCommandOperation
    where TNode : IOGraphNode, new()
{
    public OGraphCommandOperation()
    {
        this.node = new TNode();
    }
}
