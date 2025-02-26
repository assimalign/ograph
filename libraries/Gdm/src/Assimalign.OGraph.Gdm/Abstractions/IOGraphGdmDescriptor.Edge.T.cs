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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphGdmEdgeDescriptor<TSource, TTarget> HasLabel(GdmLabel label);
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IOGraphGdmEdgeDescriptor<TSource, TTarget> WithOne();
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IOGraphGdmEdgeDescriptor<TSource, TTarget> WithMany();
}