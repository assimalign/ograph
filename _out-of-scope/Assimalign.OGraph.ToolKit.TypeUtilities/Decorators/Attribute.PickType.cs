using System;
using System.Reflection;

namespace System;

/// <summary>
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class PickTypeAttribute : Attribute
{
    private string? _namespace;
    private Type? _type;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name">The name of the new class</param>
    public PickTypeAttribute(string name)
    {
        Name = name;
    }

    /// <summary>
    /// The name of the new class to be generated.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The namespace of the new class to be generated.
    /// </summary>
    public string Namespace
    {
        get
        {
            if (string.IsNullOrEmpty(_namespace))
            {
                return (_namespace = Type.Namespace!);
            }
            return _namespace!;
        }
        set
        {
            _namespace = value;
        }
    }

    /// <summary>
    /// The collection of properties to include in the generated type.
    /// </summary>
    public string[] Properties { get; set; } = [];

    /// <summary>
    /// Specifies whether to include implicit operators in the generated type.
    /// </summary>
    public bool IncludeImplicitOperators { get; set; } = true;

    /// <summary>
    /// The type information of the class to be generated.
    /// </summary>
    public Type Type
    {
        get
        {
            if (_type is null)
            {
                _type = Assembly.GetExecutingAssembly().Resolve(Name, Namespace!);
            }
            return _type;
        }
    }
}
