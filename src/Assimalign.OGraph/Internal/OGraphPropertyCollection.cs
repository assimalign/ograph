using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphPropertyCollection : List<IOGraphProperty>,
    IOGraphPropertyCollection
{
    public IOGraphProperty this[Name propertyName]
    {
        get => this.First(x => x.Name == propertyName);
        set => this.Add(value);
    }


    public IOGraphProperty Find(Name name)
    {
        return default;
    }

}
