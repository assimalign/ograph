using System;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmFunctionDescriptor
{
    IOGraphGdmFunctionDescriptor UseFunctionName(GdmName name);
    IOGraphGdmFunctionDescriptor UseType(IOGraphGdmType type);
    IOGraphGdmFunctionDescriptor UseParameter(IOGraphGdmFunctionParameter parameter);
}
