using System;
namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmOperationDescriptor
{
    IOGraphGdmOperationDescriptor HasParameter(IOGraphGdmParameter parameter);
    IOGraphGdmOperationDescriptor HasParameter(Func<IOGraphGdmGraph, IOGraphGdmParameter> parameter);
    IOGraphGdmOperationDescriptor HasReturnType(IOGraphGdmType type);
    IOGraphGdmOperationDescriptor HasReturnType(Func<IOGraphGdmGraph, IOGraphGdmType> type);
}
