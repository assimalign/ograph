using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmDescriptor
{
    IOGraphGdmElement Describe();
}


public interface IOGraphGdmDescriptor<out TElement> : IOGraphGdmDescriptor
{
    new TElement Describe();
}