using System;
using System.Linq.Expressions;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmEdgeKeyDescriptor<T>
{
    void HasReferenceKey<TMember>(Expression<Func<T, TMember>> expression);
}