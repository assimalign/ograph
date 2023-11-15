using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public abstract class OGraphVertex : IOGraphVertex
{
    public OGraphVertex()
    {
        
    }

    public Label Label => throw new NotImplementedException();

    public IOGraphType Type => throw new NotImplementedException();

    public IOGraphEdgeCollection Edges => throw new NotImplementedException();

    public IOGraphMetadata Metadata => throw new NotImplementedException();

    public IOGraphOperationCollection GetOperations()
    {
        throw new NotImplementedException();
    }

    public IOGraphPropertyCollection GetProperties()
    {
        throw new NotImplementedException();
    }
}



