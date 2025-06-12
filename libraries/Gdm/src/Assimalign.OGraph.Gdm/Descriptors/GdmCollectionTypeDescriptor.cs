using Assimalign.OGraph.Gdm.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public sealed class GdmCollectionTypeDescriptor<T> : IOGraphGdmCollectionTypeDescriptor
{
    internal GdmCollectionTypeDescriptor(GdmCollectionType collectionType)
    {

    }


    IOGraphGdmCollectionTypeDescriptor IOGraphGdmCollectionTypeDescriptor.HasName(GdmName name)
    {
        throw new NotImplementedException();
    }
    IOGraphGdmCollectionTypeDescriptor IOGraphGdmCollectionTypeDescriptor.AddMeta(string key, object value)
    {
        throw new NotImplementedException();
    }
    IOGraphGdmCollectionTypeDescriptor IOGraphGdmCollectionTypeDescriptor.HasItemType(IOGraphGdmType type)
    {
        throw new NotImplementedException();
    }

    
}
