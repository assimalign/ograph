using System;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
public interface IOGraphGdmComplexTypeDescriptor
{
    /// <summary>
    /// Adds a ty
    /// </summary>
    /// <param name="label"></param>
    /// <returns></returns>
    IOGraphGdmComplexTypeDescriptor HasLabel(Label label);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IOGraphGdmPropertyDescriptor HasProperty(Label name);
}