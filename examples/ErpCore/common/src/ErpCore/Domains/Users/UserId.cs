using System;

namespace ErpCore;

using Assimalign.OGraph;

[EntityKey(EntityKeyRuntimeType.Int)]
public partial struct UserId
{
    public partial bool IsValid(Guid value, out string message)
    {
        message = string.Empty;

        return false;
    }
}
