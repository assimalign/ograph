using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Elements;

/// <summary>
/// 
/// </summary>
public abstract class GdmElement : IOGraphGdmElement
{
    internal GdmElement()
    {
         Meta = new GdmMetadata();
    }

    /// <summary>
    /// Metadata of the element.
    /// </summary>
    public GdmMetadata Meta { get; }

    /// <summary>
    /// The element kind
    /// </summary>
    public abstract GdmElementKind ElementKind { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    /// <returns></returns>
    public virtual IEnumerable<TElement> GetElements<TElement>()
    {
        return [];
    }

    IOGraphGdmMetaCollection IOGraphGdmElement.Meta => Meta;
}
