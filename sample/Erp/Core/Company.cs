namespace Erp;

public class Company : DomainEntity<Company, CompanyId>
{
    public CompanyInfo? Info { get; set; }
    public Audit? Created { get; set; }
    public Audit? Updated { get; set; }
    public override Domain Domain => Domain.Companies;
    public override DomainEntityType EntityType => DomainEntityType.Company;
}
