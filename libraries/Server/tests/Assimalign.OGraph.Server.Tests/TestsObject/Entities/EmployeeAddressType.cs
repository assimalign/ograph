using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Server;

public record class EmployeeAddressType : EmployeeBase<EmployeeAddressType>
{
    public int? TypeId { get; set; }
    public string? Type { get; set; }
}
