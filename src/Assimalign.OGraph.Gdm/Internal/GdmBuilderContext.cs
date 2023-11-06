using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

internal class GdmBuilderContext
{
    public GdmBuilderContext(Gdm model)
    {
        Model = model;
    }

    public Gdm Model { get; }

    public void AddVertex(IOGraphGdmVertex vertex)
    {
        Model.Vertices.Add(vertex);
        AddType(vertex.Type.Definition);
    }

    public void AddType(IOGraphGdmType type)
    {
        switch (type)
        {
            case IOGraphGdmEnumType enumType:
                AddEnumType(enumType); 
                break;
            case IOGraphGdmPrimitiveType primitiveType: 
                AddPrimitiveType(primitiveType); 
                break;
            case IOGraphGdmComplexType complexType:
                AddComplexType(complexType);
                break;
            case IOGraphGdmCollectionType collectionType:
                AddCollectionType(collectionType);
                break;
            default:
                {
                    throw new InvalidOperationException($"Type {type.GetType().Name} is not allowed.");
                }
        }
    }
    public void AddEnumType(IOGraphGdmType type)
    {
        Model.Types.TryAdd(type);
    }
    public void AddPrimitiveType(IOGraphGdmPrimitiveType type)
    {
        Model.Types.TryAdd(type);
    }
    public void AddComplexType(IOGraphGdmComplexType type)
    {
        Model.Types.TryAdd(type);
        foreach (var property in type.Properties)
        {
            if (property.Type is not null)
            {
                AddType(property.Type.Definition);
            }
        }
    }
    public void AddCollectionType(IOGraphGdmCollectionType type)
    {
        Model.Types.TryAdd(type);
        AddType(type.ItemType);
    }


    public IOGraphGdmType GetTypeByName(Label name)
    {
        throw new NotImplementedException();
    }

    public IOGraphGdmType GetTypeByRuntimeType(Type type)
    {
        throw new NotImplementedException();
    }
}
