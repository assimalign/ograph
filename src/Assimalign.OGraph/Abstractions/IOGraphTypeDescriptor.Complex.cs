using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphComplexTypeDescriptor : IOGraphTypeDescriptor
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMember"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IOGraphComplexTypeDescriptor Ignore<TMember>(string name);
}


public interface IOGraphComplexTypeDescriptor<T> : IOGraphTypeDescriptor<T>
    where T : class
{
    
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMember"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IOGraphComplexTypeDescriptor<T> Ignore<TMember>(Expression<Func<T, TMember>> expression);
}
