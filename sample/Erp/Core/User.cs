using System;

namespace Erp;

public class User : DomainEntity<User, UserId>
{
    public Email Email { get; set; }
    public UserInfo? Info { get; set; }
    public Username? Username { get; set; }
    public Audit? Created { get; set; }
    public Audit? Updated { get; set; }
    public override Domain Domain => Domain.Users;
    public override DomainEntityType EntityType => DomainEntityType.User;
}
