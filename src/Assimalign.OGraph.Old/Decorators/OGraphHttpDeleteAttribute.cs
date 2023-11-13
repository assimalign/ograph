namespace Assimalign.OGraph;

public sealed class OGraphHttpDeleteAttribute : OGraphHttpMethodAttribute
{
    public OGraphHttpDeleteAttribute(string route) 
        : base("DELETE", route)
    {
    }
}
