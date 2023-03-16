namespace Assimalign.OGraph;

public class User
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public DateTime? Birthdate { get; set; }
    public UserDetails? Details { get; set; }
    public IEnumerable<UserAddress>? Addresses { get; set; }
}

