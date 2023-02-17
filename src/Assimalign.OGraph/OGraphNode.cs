using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public abstract class OGraphNode<T> : IOGraphNode<T>
{
    public Label Label { get; }

    public IOGraphEdgeCollection Edges { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public IOGraphNodePropertyCollection Properties => throw new NotImplementedException();



    public void Configure(IOGraphNodeDescriptor descriptor)
    {
        if (descriptor is IOGraphNodeDescriptor<T> typedDescriptor)
        {
            Configure(typedDescriptor);
        }

        throw new Exception("Invalid Node Descriptor");
    }


    public abstract void Configure(IOGraphNodeDescriptor<T> descriptor);

    
}
