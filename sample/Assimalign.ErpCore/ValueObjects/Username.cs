namespace Assimalign.ErpCore.Entities;

public readonly struct Username
{
    public Username(string value)
    {
        Value = value;
    }

    public readonly string Value { get; }


    public static implicit operator Username(string value) => new Username(value);
    public static implicit operator string(Username value) => value.Value;
}
