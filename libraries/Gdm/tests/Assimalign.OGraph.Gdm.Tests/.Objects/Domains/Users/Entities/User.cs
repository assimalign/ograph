namespace Assimalign.OGraph.Gdm.Tests.Objects;

public class User : Entity<User>
{
    public UserId? Id { get; set; }
    public UserInfo? Info { get; set; }
}
