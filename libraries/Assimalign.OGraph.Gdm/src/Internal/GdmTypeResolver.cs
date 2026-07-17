using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

using Elements;

internal class GdmTypeResolver
{
    private readonly GdmTypeCollection _types;

    public GdmTypeResolver(GdmTypeCollection types)
    {
        _types = types;
    }


    private static (Type, Func<GdmGraph, GdmType>)[] _knownTypes = [
        (typeof(int), (graph => new GdmInt32Type(graph)))
        ];

    public Func<GdmGraph, GdmType> Resolve(Type runtimeType)
    {
        var item = (_types as IEnumerable<GdmType>).FirstOrDefault(p => p.IsOfType(runtimeType));

        if (item is not null)
        {
            return new Func<GdmGraph, GdmType>(graph => item);
        }

        for (int i = 0; i < _knownTypes.Length; i++)
        {
            var (type, func) = _knownTypes[i];

            if (type == runtimeType)
            {
                return func;
            }
        }

        throw new Exception();
    }
}
