namespace Assimalign.OGraph;


[OGraphNode<CompanyNode>()]
public class CompanyOperations
{
    [OGraphNode<CompanyNode>()]
    [OGraphOperation("UpdateCompany")]
    [OGraphHttpPut, OGraphHttpRoute("/companies/{companyId}")]
    public async Task<IOGraphOperationResult> UpdateCompanyAsync(IOGraphOperationContext content)
    {
        throw new NotImplementedException();
    }
}
