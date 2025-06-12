
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Elements;


public class GdmOperation : GdmBindableElement, IOGraphGdmOperation
{
    public GdmOperation(GdmName name, GdmType returnType) : base(name)
    {
        ReturnType = returnType;
    }

    public GdmType ReturnType { get; }
    public GdmParameterCollection Parameters { get; } = new GdmParameterCollection();
    public sealed override GdmElementKind ElementKind { get; } = GdmElementKind.Operation;
    IOGraphGdmType? IOGraphGdmOperation.ReturnType => ReturnType;
    IOGraphGdmParameterCollection IOGraphGdmOperation.Parameters => Parameters;

    //GdmOperationAction IOGraphGdmOperation.Kind => throw new NotImplementedException();



    bool IOGraphGdmBindableElement.IsBound => throw new NotImplementedException();

}
