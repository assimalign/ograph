using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmEdgeDescriptor<T> where T : class, new()
{
    IOGraphGdmEdgeKeyDescriptor<T> WithOne();
    IOGraphGdmEdgeKeyDescriptor<T> WithMany();
}


public interface IOGraphGdmEdgeKeyDescriptor<T>
{
    void HasReferenceKey<TMember>(Expression<Func<T, TMember>> expression);
}