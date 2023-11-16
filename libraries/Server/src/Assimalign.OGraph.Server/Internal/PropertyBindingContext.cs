using Assimalign.OGraph.Gdm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;


using Syntax;

internal class PropertyBindingContext : IOGraphPropertyBindingContext
{
    internal volatile object Parent;

    public IList<IOGraphError> Errors { get; init; } = new List<IOGraphError>();
    public IOGraphGdmProperty Element { get; init; }
    public IOGraphRequest Request { get; init; }
    public IOGraphResponse Response { get; init; }
    public IServiceProvider ServiceProvider { get; init; }
    public PropertyNode Node { get; init; }

    public T GetParent<T>()
    {
        if (Parent is not T parent)
        {
            throw new InvalidOperationException();
        }

        return parent;
    }

    public T GetService<T>()
    {
        if (ServiceProvider?.GetService(typeof(T)) is T service)
        {
            return service;
        }
        throw new Exception();
    }

    public ClaimsPrincipal GetClaimsPrincipal()
    {
        throw new NotImplementedException();
    }

    IOGraphGdmElement IOGraphGdmBindingContext.Element => Element;
}