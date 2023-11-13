namespace Assimalign.OGraph;

public record class UserProfile : UserBase<UserProfile>
{
    public Guid? UserId { get; set; }
    public bool? IsEnabled { get; set; }
}
