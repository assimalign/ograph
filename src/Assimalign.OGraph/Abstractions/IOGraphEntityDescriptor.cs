using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;


/// <summary>
/// This interface is used to 
/// </summary>
public interface IOGraphEntityDescriptor<T>
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMember"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    IOGraphEntityDescriptor<T> Ignore<TMember>(Expression<Func<T, TMember>> expression);
}
