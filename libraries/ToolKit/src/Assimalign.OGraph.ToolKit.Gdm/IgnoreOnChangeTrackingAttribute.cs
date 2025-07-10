using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;


[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class IgnoreOnChangeTrackingAttribute : Attribute
{
}