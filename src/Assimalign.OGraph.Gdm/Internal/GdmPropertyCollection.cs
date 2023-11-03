using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmPropertyCollection : List<IOGraphGdmProperty>,
    IOGraphGdmPropertyCollection
{
    public IOGraphGdmProperty this[Label name] => throw new NotImplementedException();
}
