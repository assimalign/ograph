using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Elements;

public class GdmMemberCollection : List<IOGraphGdmMember>, IOGraphGdmMemberCollection
{
    public IOGraphGdmMember this[Label label] => this.Find(label);

    public IOGraphGdmFunction GetFunction(Label label)
    {
        return this.OfType<IOGraphGdmFunction>().Find(label);
    }

    public IOGraphGdmProperty GetProperty(Label label)
    {
        return this.OfType<IOGraphGdmProperty>().Find(label);
    }
}
