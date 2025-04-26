using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public readonly struct GdmPathValue
{
    public GdmPathValue(string value)
    {
        
    }

    public GdmStepValue[] Steps { get; }



    #region Operators


    public static implicit operator GdmPathValue(string value)
    {
        return new GdmPathValue(value);
    }

    public static implicit operator string(GdmPathValue value)
    {
        return value.ToString()!;
    }



    #endregion
}