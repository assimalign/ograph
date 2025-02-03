using System;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmBuilder
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    IOGraphGdmBuilder BeforeBuild(Action<IOGraphGdm> configure);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    IOGraphGdmBuilder AfterBuild(Action<IOGraphGdm> configure);

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

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TTarget"></typeparam>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphGdmBuilder AddEdge<TSource, TTarget>(Label label);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IOGraphGdm Build();
}