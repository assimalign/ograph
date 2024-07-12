using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp;

public partial struct Name
{
    public partial bool IsValid(string value, out string? error);


    public partial bool IsValid(string value, out string? error)
    {
        error = null;

        return true;
    }
}
