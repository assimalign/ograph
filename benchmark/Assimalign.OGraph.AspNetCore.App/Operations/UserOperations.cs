namespace Assimalign.OGraph;


[OGraphVertex<UserVertex>]
public class UserOperations
{
    [OGraphHttpPut("/companies/{companyId}")]
    [OGraphHttpOperation("UpdateCompany")]
    public async Task<IOGraphResult> UpdateCompanyAsync([OGraphService] object someService )
    {
        throw new NotImplementedException();
    }
}
