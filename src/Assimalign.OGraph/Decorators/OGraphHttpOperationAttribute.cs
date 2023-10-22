using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;


[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class OGraphHttpOperationAttribute : Attribute
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name">The name of the operation.</param>
    public OGraphHttpOperationAttribute(string name)
    {
        this.Name = name;
    }

    /// <summary>
    /// The Operation Name.
    /// </summary>
    public Name Name { get; }
}
