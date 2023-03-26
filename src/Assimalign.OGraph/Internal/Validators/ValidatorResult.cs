using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Internal;

internal class ValidatorResult
{
    public ValidatorResult()
    {
        
    }
    public bool IsValid => !Errors.Any();
    public IEnumerable<ValidatorError> Errors { get; }
}
