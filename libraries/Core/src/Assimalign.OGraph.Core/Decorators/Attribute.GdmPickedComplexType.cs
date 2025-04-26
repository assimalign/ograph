using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Assimalign.OGraph;

/// <summary>
/// Create
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class GdmPickedComplexTypeAttribute : Attribute
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    public GdmPickedComplexTypeAttribute(string name)
    {
        Name = name;
    }

    /// <summary>
    /// The name of the new type.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The collection of properties to include in the generated type.
    /// </summary>
    public string[] Include { get; set; } = [];
}