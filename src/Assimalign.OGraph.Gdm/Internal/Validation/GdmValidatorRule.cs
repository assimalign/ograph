using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Internal;

internal abstract class GdmValidatorRule
{


    public abstract void OnValidate(GdmValidatorContext context);
}
