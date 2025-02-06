using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IOGraphGdmFunctionDescriptor<T>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphGdmFunctionDescriptor<T> UseFunctionName(Label label);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TGdmType"></typeparam>
    /// <returns></returns>
    IOGraphGdmFunctionDescriptor<T> UseType<TGdmType>() where TGdmType : IOGraphGdmType, new();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    IOGraphGdmFunctionDescriptor<T> UseType(IOGraphGdmType type);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TGdmType"></typeparam>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphGdmFunctionDescriptor<T> UseParameter<TGdmType>(Label label) where TGdmType : IOGraphGdmType, new();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    IOGraphGdmFunctionDescriptor<T> UseParameter(Label label, IOGraphGdmType type);
}
