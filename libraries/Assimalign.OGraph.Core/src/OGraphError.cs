namespace Assimalign.OGraph;

public sealed class OGraphError : IOGraphError
{
    public OGraphError()
    {
        Details = new();
    }
    public OGraphError(string code, string message) : this()
    {
        Code = code;
        Message = message;
    }
    public string? Code { get; set; }
    public string? Message { get; set; }
    public OGraphErrorDetailsCollection Details { get; }
    IOGraphErrorDetailsCollection IOGraphError.Details => Details;
}