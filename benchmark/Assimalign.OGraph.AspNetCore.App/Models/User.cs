namespace Assimalign.OGraph;

public record class User : UserBase<User>
{
    public Guid? UserId { get; set; }
    public UserDetails? Details { get; set; }
}