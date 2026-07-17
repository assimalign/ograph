using System;

namespace Assimalign.OGraph.Gdm;


[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class GdmOmittedComplexTypeAttribute : Attribute
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    public GdmOmittedComplexTypeAttribute(string name)
    {
        Name = name;
    }

    /// <summary>
    /// 
    /// </summary>
    public string Name { get; }
}
