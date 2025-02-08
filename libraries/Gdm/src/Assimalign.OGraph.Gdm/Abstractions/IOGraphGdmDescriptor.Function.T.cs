using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmFunctionDescriptor<T>
{
    IOGraphGdmFunctionDescriptor<T> UseFunctionName(Label label);
    //IOGraphGdmFunctionDescriptor<T> UseType<TGdmType>() where TGdmType : IOGraphGdmType, new();
    IOGraphGdmFunctionDescriptor<T> UseType(IOGraphGdmType type);
    IOGraphGdmFunctionDescriptor<T> UseType(Func<IOGraphGdmType, IOGraphGdmGraph> type);
    IOGraphGdmFunctionDescriptor<T> UseParameter<TType>(Label label) where TType : IOGraphGdmType, new();
    IOGraphGdmFunctionDescriptor<T> UseParameter(Label label, IOGraphGdmType type);
}
