using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmEdgeDescriptor<TSource, TTarget> 
    where TSource : class, new()
    where TTarget : class, new()
{
    IOGraphGdmEdgeDescriptor<TSource, TTarget> HasLabel(Label label);
    IOGraphGdmEdgeDescriptor<TSource, TTarget> WithOne();
    IOGraphGdmEdgeDescriptor<TSource, TTarget> WithMany();
}