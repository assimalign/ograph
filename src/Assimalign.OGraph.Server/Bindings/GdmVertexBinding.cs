using Assimalign.OGraph.Gdm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphVertexBindingDescriptor<T>
{
    IOGraphVertexPropertyBindingDescriptor<T> Property<TMember>(Expression<Func<T, TMember>> expression);
}


public interface IOGraphVertexPropertyBindingDescriptor<T>
{
    IOGraphVertexPropertyBindingDescriptor<T> UseResolver(OGraphGdmPropertyBindingResolver resolver);
}

public abstract class OGraphVertexBinding<T> : GdmVertex<T> where T : class, new()
{

    protected virtual void Configure(IOGraphVertexBindingDescriptor<T> descriptor)
    {
        
    }
}

public interface IOGraphGdmPropertyBindingDescriptor<T>
{
    
}

public interface IOGraphGdmVertexBindingDescriptor<T> 
    where T : class, new()
{
    IOGraphGdmPropertyBindingDescriptor<TMember> Property<TMember>(Expression<Func<T, TMember>> expression);
}



public abstract class GdmEntityTypeBinding<T> : GdmEntityType<T> 
    where T : class, new()
{
    
}

public abstract class GdmComplexTypeBinding<T> : GdmComplexType<T> 
    where T : class, new()
{

}

public abstract class GdmVertexBinding<T> : GdmVertex<T> 
    where T : class, new()
{
    protected abstract void Configure(IOGraphGdmVertexBindingDescriptor<T> descriptor);
}