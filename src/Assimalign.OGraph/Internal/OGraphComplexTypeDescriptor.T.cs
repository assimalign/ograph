using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class OGraphComplexTypeDescriptor<T> : IOGraphComplexTypeDescriptor<T>
{
    public IOGraphPropertyDescriptor HasProperty(Name name)
    {
        throw new NotImplementedException();
    }

    public IOGraphPropertyDescriptor<TProperty> HasProperty<TProperty>(System.Linq.Expressions.Expression<Func<T, TProperty>> expression)
    {
        throw new NotImplementedException();
    }

    public IOGraphComplexTypeDescriptor<T> Ignore(Name name)
    {
        throw new NotImplementedException();
    }

    public IOGraphComplexTypeDescriptor<T> Ignore<TProperty>(System.Linq.Expressions.Expression<Func<T, TProperty>> expression)
    {
        throw new NotImplementedException();
    }
}
