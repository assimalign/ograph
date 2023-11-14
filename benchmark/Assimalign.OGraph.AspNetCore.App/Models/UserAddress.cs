namespace Assimalign.OGraph;

public record class UserAddress : UserBase<UserAddress>
{
    public Guid? UserId { get; set; }
    public Guid? AddressId { get; set; }
    public Address? Address { get; set; }
    public AddressType AddressType { get; set; }
}