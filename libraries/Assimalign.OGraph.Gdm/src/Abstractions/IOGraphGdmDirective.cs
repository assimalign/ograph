using System;
using System.Collections.Generic;
using System.Text;

namespace Assimalign.OGraph.Gdm;

public interface IOGraphGdmDirective
{
    IOGraphGdmDirectiveType DirectiveType { get; }
}
