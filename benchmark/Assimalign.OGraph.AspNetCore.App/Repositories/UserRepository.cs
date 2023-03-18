namespace Assimalign.OGraph;

public class UserRepository : IRepository<User>
{

    private static User[] users => new User[]
    {
        new () { FirstName  = "Chase" }
    };



    public IQueryable<User> Queryable => users.AsQueryable();

    IQueryable IRepository.Queryable => this.Queryable;
}
