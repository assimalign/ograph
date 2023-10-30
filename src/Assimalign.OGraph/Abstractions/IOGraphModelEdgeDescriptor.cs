using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphModelEdgeDescriptor
{
    IOGraphModelEdgeDescriptor WithMany<T>() where T : class, new();
    IOGraphModelEdgeDescriptor WithOne<T>() where T : class, new();
}
