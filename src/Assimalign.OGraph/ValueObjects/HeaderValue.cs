using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public readonly struct HeaderValue
{

    public HeaderValue()
    {
        
    }

    public string Value { get; }


    public static bool operator ==(HeaderValue left, HeaderValue right) => left.Value == right.Value;
    public static bool operator !=(HeaderValue left, HeaderValue right) => left.Value != right.Value;


    public static bool operator ==(string left, HeaderValue right) => left == right.Value;
    public static bool operator !=(string left, HeaderValue right) => left != right.Value;


    public static bool operator ==(HeaderValue left, string right) => left.Value == right;
    public static bool operator !=(HeaderValue left, string right) => left.Value != right;
}
