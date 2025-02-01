using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpCore;

public class Audit
{
    public UserId UserId { get; set; }
    public DateTimeOffset Timestamp { get; set; }
}
