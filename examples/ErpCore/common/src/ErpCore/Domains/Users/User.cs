namespace ErpCore;

public class User
{
    public UserId Id { get; set; }
    public UserInfo? Info { get; set; }
    public Username Username { get; set; }
    public Audit? Created { get; set; }
    public Audit? Updated { get; set; }
}
