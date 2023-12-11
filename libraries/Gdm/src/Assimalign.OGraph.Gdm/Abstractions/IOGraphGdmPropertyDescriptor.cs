using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmPropertyDescriptor
{
    IOGraphGdmPropertyDescriptor UsePropertyName(Label label);
    IOGraphGdmPropertyDescriptor UseType<TGdmType>() where TGdmType : IOGraphGdmType, new();
    IOGraphGdmPropertyDescriptor UseType(IOGraphGdmType type);
    IOGraphGdmPropertyDescriptor UseMetadata(Label key, object value);
    IOGraphGdmPropertyDescriptor IsComputed();
    IOGraphGdmPropertyDescriptor IsRequired();
    IOGraphGdmPropertyDescriptor UseGetter(GdmPropertyGetter getter);
    IOGraphGdmPropertyDescriptor UseSetter(GdmPropertySetter setter);
}
