using System;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmFunctionDescriptor
{
    IOGraphGdmFunctionDescriptor UseFunctionName(GdmLabel label);
    IOGraphGdmFunctionDescriptor UseType<TType>() where TType : IOGraphGdmType;
    IOGraphGdmFunctionDescriptor UseType(IOGraphGdmType type);
    IOGraphGdmFunctionDescriptor UseType(Func<IOGraphGdmType, IOGraphGdmGraph> type);
    IOGraphGdmFunctionDescriptor UseParameter<TType>(GdmLabel label) where TType : IOGraphGdmType, new();
    IOGraphGdmFunctionDescriptor UseParameter(GdmLabel label, IOGraphGdmType type);
}
