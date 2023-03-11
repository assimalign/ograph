using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphNodeDescriptor : IOGraphNodeDescriptor
{

    private readonly IOGraphNode node;

    public OGraphNodeDescriptor(IOGraphNode node)
    {
        if (node is null)
        {
            throw new ArgumentNullException(nameof(node));
        }
            
        this.node = node;
    }



    public IOGraphNodeDescriptor HasLabel(Label label)
    {
        throw new NotImplementedException();
    }

    public IOGraphNodeDescriptor HasMetadata(string key, object value)
    {
        throw new NotImplementedException();
    }

    public IOGraphPropertyDescriptor HasProperty(Name name)
    {
        if (node.Properties.TryGet(name, out var existing) && existing is not null)
        {
            return new OGraphPropertyDescriptor(existing);
        }

        var property = new OGraphProperty()
        {
            Name = name
        };

        node.Properties.Add(property);

        return new OGraphPropertyDescriptor(property);
    }
}


