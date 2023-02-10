using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public interface IOGraphTypeDescriptor
{
}

public interface IOGraphTypeDescriptor<T>
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphTypeDescriptor<T> UseResolver(IOGraphResolver resolver);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resolver"></param>
    /// <returns></returns>
    IOGraphTypeDescriptor<T> UseResolver(OGraphTypeResolver<T> resolver);
}
