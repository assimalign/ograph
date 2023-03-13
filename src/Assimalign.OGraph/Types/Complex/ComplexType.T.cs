using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public class ComplexType<T> : ComplexType
{
    public ComplexType()
    {
        base.RuntimeType    = typeof(T);
        base.TypeName       = typeof(T).Name;
    }
    protected virtual void Configured(IOGraphComplexTypeDescriptor<T> descriptor)
    {

    }
}
