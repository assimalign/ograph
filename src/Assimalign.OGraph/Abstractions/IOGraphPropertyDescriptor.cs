using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphPropertyDescriptor
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    /// <returns></returns>
    IOGraphPropertyDescriptor UseType<TType>() where TType : IOGraphType, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns></returns>
    IOGraphPropertyDescriptor UseMiddleware(OGraphPropertyMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middleware"></param>
    /// <returns></returns>
    IOGraphPropertyDescriptor UseMiddleware(IOGraphPropertyMiddleware middleware);
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMiddleware"></typeparam>
    /// <returns></returns>
    IOGraphPropertyDescriptor UseMiddleware<TMiddleware>() where TMiddleware : IOGraphPropertyMiddleware, new();
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    IOGraphPropertyDescriptor UseMetadata(string key, object value);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphPropertyDescriptor UseResolver(IOGraphPropertyResolver resolver);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphPropertyDescriptor UseResolver(OGraphPropertyResolver resolver);
}


