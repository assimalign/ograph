namespace Assimalign.OGraph.AspNetCore;


public class UserType : ComplexType<User>
{
    protected override void Configure(IOGraphComplexTypeDescriptor<User> descriptor)
    {
        
    }
}

public class User
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public UserDetails Details { get; set; }
}


public class UserDetails
{
    public string Ssn { get; set; }
    public DateTime Birthdate { get; set; }
}