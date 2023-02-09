using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphServerBuilder
{
    IOGraphServerBuilder UseOGraphModel();
    IOGraphServerBuilder UseOGraphExecutor();

    IOGraphServer Build();
}
