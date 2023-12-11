using System;
using System.Linq.Expressions;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmBuilder
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TGdmType"></typeparam>
    /// <returns></returns>
    IOGraphGdmBuilder AddType<TGdmType>() where TGdmType : class, IOGraphGdmType, new();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    IOGraphGdmBuilder AddType(IOGraphGdmType type);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TGdmVertex"></typeparam>
    /// <returns></returns>
    IOGraphGdmBuilder AddVertex<TGdmVertex>() where TGdmVertex : IOGraphGdmVertex, new();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="vertex"></param>
    /// <returns></returns>
    IOGraphGdmBuilder AddVertex(IOGraphGdmVertex vertex);
    

    //IOGraphGdmBuilder AddType<T>(Action<IOGraphGdmComplexTypeDescriptor<T>> configure) where T : class, new();

    //IOGraphGdmBuilder AddVertex<T>(Action<IOGraphGdmEntityTypeDescriptor<T>> configure)  where T : class, new();
    //IOGraphGdmBuilder AddVertex(Action<IOGraphGdmVertexDescriptor> configure);
    //IOGraphGdmBuilder AddVertex<T>(Action<IOGraphGdmVertexDescriptor<T>> configure) where T : class, new();
    IOGraphGdm Build();
}