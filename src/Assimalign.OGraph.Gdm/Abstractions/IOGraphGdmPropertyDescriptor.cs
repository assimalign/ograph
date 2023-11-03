using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmPropertyDescriptor
{
    IOGraphGdmPropertyDescriptor UseType<TType>() where TType : IOGraphGdmType, new();
}
