using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

public readonly struct GdmStepValue
{

    public GdmStepValue(string value)
    {
        
    }


    /// <summary>
    /// 
    /// </summary>
    public string Value { get; }


    #region Methods

    public GdmLabel GetEdge()
    {
        return default;
    }
    
    public GdmLabel GetSource()
    {
        return default;
    }

    public GdmLabel GetTarget()
    {
        return default;
    }



    #endregion


    #region Overloads


    #endregion


    #region Operators

    public static implicit operator GdmStepValue(string value)
    {
        return new GdmStepValue(value);
    }

    public static implicit operator string(GdmStepValue value)
    {
        return value.Value;
    }



    #endregion

}
