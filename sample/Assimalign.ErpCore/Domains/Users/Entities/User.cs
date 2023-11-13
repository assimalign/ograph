using System;

namespace Assimalign.ErpCore.Entities;

public record class User : DomainEntity<User>
{
    private Username? username;

    /// <summary>
    /// 
    /// </summary>
    public Guid? UserId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Username? Username
    {
        get => username;
        set
        {
            BeginPropertyChange();
            username = value;
            EndPropertyChange();
        }
    }
}
