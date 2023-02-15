using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphNodeDescriptor
{
   
}

public interface IOGraphNodeDescriptor<T>
{

    /// <summary>
    /// Specify a property to be ignored.
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IOGraphNodeDescriptor<T> Ignore<TProperty>(Expression<Func<T, TProperty>> expression);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IOGraphNodeDescriptor<T> HasKey<TProperty>(Expression<Func<T, TProperty>> expression);

    IOGraphNodePropertyDescriptor HasProperty(Name name);


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IOGraphNodePropertyDescriptor<TProperty> HasProperty<TProperty>(Expression<Func<T, TProperty>> expression);


    IOGraphEdgeDescriptor<TProperty> HasEdge<TProperty>(Expression<Func<T, TProperty>> expression);



}
