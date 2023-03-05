using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphSerializationException : OGraphException
{
    public OGraphSerializationException(string message) : base(message)
    {
        
    }
}
