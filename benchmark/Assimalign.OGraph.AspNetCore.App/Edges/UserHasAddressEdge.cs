using Microsoft.EntityFrameworkCore;

namespace Assimalign.OGraph;

public class UserHasAddressEdge
{
    public UserHasAddressEdge()
    {
        
    }

    public DbSet<User> MyProperty { get; set; }
}
