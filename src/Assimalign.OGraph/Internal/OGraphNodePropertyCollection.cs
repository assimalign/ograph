using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphNodePropertyCollection : List<IOGraphNodeProperty>,
    IOGraphNodePropertyCollection
{
    public IOGraphNodeProperty this[Name propertyName]
    {
        get => this.First(x => x.PropertyName == propertyName);
        set => this.Add(value);
    }

}
