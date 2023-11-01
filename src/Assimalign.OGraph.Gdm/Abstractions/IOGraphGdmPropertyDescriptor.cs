using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmPropertyDescriptor
{
    IOGraphGdmPropertyDescriptor UseGetter<TGetter>() where TGetter : IOGraphGdmPropertyGetter, new();
    IOGraphGdmPropertyDescriptor UseSetter<TSetter>() where TSetter : IOGraphGdmPropertySetter, new();
    IOGraphGdmPropertyDescriptor UseType<TType>() where TType : IOGraphGdmType, new();
}
