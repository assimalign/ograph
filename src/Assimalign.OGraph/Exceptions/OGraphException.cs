using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public abstract class OGraphException : Exception
{
    public OGraphException(string message) : base(message)
    {
        
    }
    public OGraphException(string message, Exception innerException) : base(message, innerException)
    {
        
    }
}
