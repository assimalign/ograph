using Assimalign.OGraph.Gdm;

namespace ErpCore;


[GdmEntityType]
[GdmPickedComplexType("UserCreateInput", Include = ["Info", "Username"])]
[GdmPickedComplexType("UserUpdateInput", Include = ["Info"])]
public partial class User
{
    public UserId Id { get; set; }
    public UserInfo? Info { get; set; }
    public Username Username { get; set; }
    public Audit? Created { get; set; }
    public Audit? Updated { get; set; }
}
