using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public abstract class OGraphType : IOGraphType
{
    public Name TypeName => throw new NotImplementedException();
    public OGraphTypeIdentifier TypeIdentifier => throw new NotImplementedException();


}
