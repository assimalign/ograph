using System;
using System.Collections.Generic;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmTypeCollection : List<IOGraphGdmType>,
    IOGraphGdmTypeCollection
{
    public IOGraphGdmType this[Label name]
    {
        get
        {
            if (TryGet(name, out var type))
            {
                return type!;
            }
            throw new KeyNotFoundException();
        }
    }

    public bool TryAdd(IOGraphGdmType type)
    {
        if (type is null)
        {
            throw new ArgumentNullException(nameof(type));
        }
        if (!Contains(type))
        {
            Add(type);
            return true;
        }
        return false;
    }

    public bool TryGet(Label name, out IOGraphGdmType? type)
    {
        type = null;
        foreach (var gdmType in this)
        {
            if (gdmType.Label == name)
            {
                type = gdmType;
                return true;
            }
        }
        return false;
    }

    public new bool Contains(IOGraphGdmType type)
    {
        foreach (var item in this)
        {
            if (item.Label == type.Label)
            {
                return true;
            }
        }
        return false;
    }
}