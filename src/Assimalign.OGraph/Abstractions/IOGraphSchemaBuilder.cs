using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public interface IOGraphSchemaBuilder
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    IOGraphSchemaBuilder AddEntity<T>();
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name">Override the default name of the entity. <see cref="typeof(T).Name"/></param>
    /// <returns></returns>
    IOGraphSchemaBuilder AddEntity<T>(string name);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    IOGraphSchemaBuilder AddEntity(Type type);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    IOGraphSchemaBuilder AddEntity(Type type, string name);
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IOGraphSchema Build();
}
