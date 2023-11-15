namespace Assimalign.OGraph;

public sealed class OGraphHttpPostAttribute : OGraphHttpMethodAttribute
{
    public OGraphHttpPostAttribute(string route) 
        : base("POST", route)
    {
    }
}
