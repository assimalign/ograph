using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm;

/// <summary>
/// 
/// </summary>
/// <param name="instance">The instance in which to retrieve the property to value.</param>
/// <returns>The value of the property.</returns>
public delegate object GdmPropertyGetter(object instance);