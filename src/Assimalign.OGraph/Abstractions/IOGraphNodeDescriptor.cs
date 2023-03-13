using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphNodeDescriptor
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphNodeDescriptor UseLabel(Label label);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    IOGraphNodeDescriptor UseMetadata(string key, object value);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    /// <remarks>
    /// The type being binded to the node should be a complex type.
    /// </remarks>
    IOGraphNodeDescriptor UseType(IOGraphType type);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    /// <returns></returns>
    IOGraphNodeDescriptor UseType<TType>() where TType : IOGraphType, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configure"></param>
    /// <returns></returns>
    IOGraphNodeDescriptor UseType(Action<IOGraphComplexTypeDescriptor> configure);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="configure"></param>
    /// <returns></returns>
    IOGraphNodeDescriptor UseType<T>(Action<IOGraphComplexTypeDescriptor<T>> configure);
}


