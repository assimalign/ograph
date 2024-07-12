namespace Erp;

public class EmployeeAddressType : DomainEntity<EmployeeAddressType, int>
{
    public int? TypeId { get; set; }
    public string? Type { get; set; }
    public override Domain Domain => throw new System.NotImplementedException();
    public override DomainEntityType EntityType => throw new System.NotImplementedException();
}
