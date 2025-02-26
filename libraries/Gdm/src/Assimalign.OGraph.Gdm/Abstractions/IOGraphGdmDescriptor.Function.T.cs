using System;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmFunctionDescriptor<T>
{
    IOGraphGdmFunctionDescriptor<T> UseFunctionName(GdmLabel label);
    IOGraphGdmFunctionDescriptor<T> UseType<TType>() where TType : IOGraphGdmType;
    IOGraphGdmFunctionDescriptor<T> UseType(IOGraphGdmType type);
    IOGraphGdmFunctionDescriptor<T> UseType(Func<IOGraphGdmType, IOGraphGdmGraph> type);
    IOGraphGdmFunctionDescriptor<T> UseParameter<TType>(GdmLabel label) where TType : IOGraphGdmType, new();
    IOGraphGdmFunctionDescriptor<T> UseParameter(GdmLabel label, IOGraphGdmType type);
}
