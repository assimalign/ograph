using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Execution.Internal;


internal class OGraphResponse : IOGraphResponse
{
    public int StatusCode => throw new NotImplementedException();

    public IOGraphHeaderCollection Headers => throw new NotImplementedException();

    public Stream Body => throw new NotImplementedException();
}
