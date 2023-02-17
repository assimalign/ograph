using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Execution;

public interface IOGraphRequest
{


    IOGraphHeaderCollection Headers { get; }
    Stream Body { get; }
}
