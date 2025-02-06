using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace System;

#if NET6_0
[RequiresPreviewFeatures]
#endif
public interface IImplicitOperator<TSelf, TResult> where TSelf : IImplicitOperator<TSelf, TResult>?
{
    static abstract implicit operator TResult(TSelf self);
}

