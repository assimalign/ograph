using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public abstract class OGraphNode<TType> : OGraphNode
    where TType : IOGraphType, new()
{
    public OGraphNode()
    {
        base.type = new TType();

        if (base.label == default)
        {
            label = new Label(type.TypeName);
        }
    }
}