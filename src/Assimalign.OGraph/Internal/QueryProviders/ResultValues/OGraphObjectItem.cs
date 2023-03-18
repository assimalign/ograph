using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal readonly struct OGraphObjectItem
{
    private readonly object value;

    public OGraphObjectItem(Name propertyName, OGraphCollection value)
    {
        this.PropertyName = propertyName;
        this.value = value;
    }
    public OGraphObjectItem(Name propertyName, Name propertyAlias, OGraphCollection value)
    {
        this.PropertyName = propertyName;
        this.value = value;
    }
    public OGraphObjectItem(Name propertyName, OGraphObject value)
    {
        this.PropertyName = propertyName;
        this.value = value;
    }
    public OGraphObjectItem(Name propertyName, Name propertyAlias, OGraphObject value)
    {
        this.PropertyName = propertyName;
        this.value = value;
    }
    public OGraphObjectItem(Name propertyName, OGraphValue value)
    {
        this.PropertyName = propertyName;
        this.value = value;
    }
    public OGraphObjectItem(Name propertyName, Name propertyAlias, OGraphValue value)
    {
        this.PropertyName = propertyName;
        this.value = value;
    }

    /// <summary>
    /// 
    /// </summary>
    public Name? PropertyAlias { get; }
    /// <summary>
    /// 
    /// </summary>
    public Name PropertyName { get; }
    
    /// <summary>
    /// Specifies the type of the property value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public bool IsCollectionType(out OGraphCollection value) => IsMatch<OGraphCollection>(out value);
    public bool IsComplexType(out OGraphObject value) => IsMatch<OGraphObject>(out value);
    public bool IsPrimitiveType(out OGraphValue value) => IsMatch<OGraphValue>(out value);


    private bool IsMatch<T>(out T? value)
    {
        value = default;

        if (this.value is T item)
        {
            value = item;
            return true;
        }

        return false;
    }
}
