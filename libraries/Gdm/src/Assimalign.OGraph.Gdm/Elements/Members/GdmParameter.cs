using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Elements;

public class GdmParameter : IOGraphGdmParameter
{
    public GdmParameter()
    {
        
    }
    public Label Label => throw new NotImplementedException();
    public IOGraphGdmType Type => throw new NotImplementedException();
    public bool IsRequired => throw new NotImplementedException();
    public GdmElementKind ElementKind { get; } = GdmElementKind.Parameter;
    public IOGraphGdmMetadata Meta { get; } = new GdmMetadata();
}
