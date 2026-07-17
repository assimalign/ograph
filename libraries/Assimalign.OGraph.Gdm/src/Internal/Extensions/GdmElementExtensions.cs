using System;
using System.Collections.Generic;
using System.Text;

namespace Assimalign.OGraph.Gdm.Internal;

using Elements;

internal static class GdmElementExtensions
{

    extension <T>(GdmEntityType<T> entity)
    {
        public GdmListType<T> AsListType()
        {
            return default;
        }
    }
}
