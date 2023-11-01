using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmPropertyDescriptor<T>
{
    IOGraphGdmPropertyDescriptor<T> UseGetter<TGetter>() where TGetter : IOGraphGdmPropertyGetter, new();
    IOGraphGdmPropertyDescriptor<T> UseSetter<TSetter>() where TSetter : IOGraphGdmPropertySetter, new();
    IOGraphGdmPropertyDescriptor<T> UseName(Label label);
}
