using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public class OGraphHttpPutAttribute : OGraphHttpMethodAttribute
{
    public OGraphHttpPutAttribute(string route) : base("PUT", route)
    {
        
    }
}
