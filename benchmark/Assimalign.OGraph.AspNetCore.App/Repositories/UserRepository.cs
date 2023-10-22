namespace Assimalign.OGraph;

public class UserRepository : IRepository<User>
{

    private static User[] users => new User[]
    {
        //new () { FirstName  = "Chase" },
        //new () { FirstName  = "John", LastName = "Doe" },
        //new () { FirstName  = "Jane", LastName = "Doe", MiddleName = "Foster" }
    };



    public IQueryable<User> Queryable => users.AsQueryable();

    IQueryable IRepository.Queryable => this.Queryable;
}
