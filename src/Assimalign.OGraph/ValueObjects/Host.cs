using System;
using System.Globalization;

namespace Assimalign.OGraph;

/// <summary>
/// 
/// </summary>
public readonly struct Host
{
    public Host(string value)
    {
        this.Value = value;
    }
    public Host(string host, int port)
    {
        if (host == null)
        {
            throw new ArgumentNullException(nameof(host));
        }
        if (port <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(port), "");
        }

        int index;
        if (!host.Contains('[')
            && (index = host.IndexOf(':')) >= 0
            && index < host.Length - 1
            && host.IndexOf(':', index + 1) >= 0)
        {
            // IPv6 without brackets ::1 is the only type of host with 2 or more colons
            host = $"[{host}]";
        }

        this.Value = host + ":" + port.ToString(CultureInfo.InvariantCulture);
        this.Port = port;
    }

    public string Value { get; }
    public int? Port { get; }

    public override string ToString()
    {
        return Value;
    }
}
