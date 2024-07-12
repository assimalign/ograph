using Assimalign.OGraph;

namespace Erp;

[ValueObject()]
public partial struct Email
{
    public Email(string value)
    {
        Value = value;
    }

    public string Value { get; }
}
