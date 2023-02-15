using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphEdgeDescriptor
{
    
}

public interface IOGraphEdgeDescriptor<T>
{
    IOGraphEdgeDescriptor<T> HasResolver(OGraphEdgeResolver<T> resolver);
}
