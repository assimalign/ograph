using System;

namespace Assimalign.OGraph;

public static class OGraphVertexDescriptorExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="descriptor"></param>
    /// <param name="labels"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static IOGraphVertexDescriptor<T> HasLabels<T>(this IOGraphVertexDescriptor<T> descriptor, params string[] labels)
        where T : class, new()
    {
        if (labels is null)
        {
            throw new ArgumentNullException(nameof(labels));
        }
        if (labels.Length == 0)
        {
            throw new ArgumentException("labels cannot be empty. At least one label is required");
        }
        foreach (var label in labels)
        {
            descriptor.HasLabels(label);
        }
        return descriptor;
    }
}
