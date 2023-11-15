using Assimalign.OGraph.Gdm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class PropertyBindingContext : IOGraphPropertyBindingContext, IOGraphPropertyBindingResolverContext
{



    internal volatile object Parent;

    public IList<IOGraphError> Errors { get; init; } = new List<IOGraphError>();
    public IOGraphGdmProperty Element { get; init; }
    public IOGraphRequest Request { get; init; }
    public IOGraphResponse Response { get; init; }

    public T GetParent<T>()
    {
        if (Parent is not T parent)
        {
            throw new InvalidOperationException();
        }

        return parent;
    }
    
    IOGraphGdmElement IOGraphGdmBindingContext.Element => Element;
}
