using System;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmGraphDescriptor
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    IOGraphGdmGraphDescriptor AddType(IOGraphGdmType type);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    IOGraphGdmGraphDescriptor AddType(Func<IOGraphGdmGraph, IOGraphGdmType> configure);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="vertex"></param>
    /// <returns></returns>
    IOGraphGdmGraphDescriptor AddVertex(IOGraphGdmVertex vertex);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    IOGraphGdmGraphDescriptor AddVertex(Func<IOGraphGdmGraph, IOGraphGdmVertex> configure);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TTarget"></typeparam>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphGdmGraphDescriptor AddEdge<TSource, TTarget>(Label label);
}
