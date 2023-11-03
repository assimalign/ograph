using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph;

public abstract record class OGraphResult : Either<IOGraphErrorResult, IOGraphQueryResult, IOGraphPropertyResult>
{
    public OGraphResult(IOGraphErrorResult value)
        : base(value) { }
    public OGraphResult(IOGraphQueryResult value)
        : base(value) { }
    public OGraphResult(IOGraphPropertyResult value)
        : base(value) { }
    protected OGraphResult(Either<IOGraphErrorResult, IOGraphQueryResult, IOGraphPropertyResult> original)
        : base(original) { }


    public static IOGraphPropertyResult PropertyValue(object value)
    {
        throw new NotImplementedException();
    }
    
    public static OGraphResult Query(IQueryable queryable)
    {
        throw new NotImplementedException();
    }
    public static IOGraphResult NotFound(string message)
    {
        throw new NotImplementedException();
    }
    public static IOGraphResult InternalError(string message)
    {
        throw new NotImplementedException();
    }
    public static IOGraphResult Unauthorized(string message)
    {
        throw new NotImplementedException();
    }
}