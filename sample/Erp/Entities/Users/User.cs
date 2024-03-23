using System;

namespace Erp;

public class User : EntityBase<User>
{
    public UserId? UserId { get; set; }
    public Username? Username { get; set; }
    public UserInfo? Info { get; set; }
}
