using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.ErpCore.Vertices;

using Assimalign.OGraph;
using Assimalign.ErpCore.Types;

public class EmployeeVertex : Vertex<EmployeeType>
{
    protected override void Configure(IVertexDescriptor descriptor)
    {
        descriptor.HasLabel("users");
    }
}


public abstract class Vertex<TType> 
    where TType : IOGraphComplexType
{
    protected virtual void Configure(IVertexDescriptor descriptor) { }
}

public interface IVertexDescriptor
{
    IVertexDescriptor HasLabel(Label label);
}