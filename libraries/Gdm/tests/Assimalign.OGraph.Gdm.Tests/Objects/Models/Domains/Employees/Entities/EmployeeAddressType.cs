using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests.Objects;

public class EmployeeAddressType : EmployeeBase<EmployeeAddressType>
{
    public int? TypeId { get; set; }
    public string? Type { get; set; }
}
