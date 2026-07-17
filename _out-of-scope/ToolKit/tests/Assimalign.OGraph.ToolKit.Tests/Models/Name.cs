using System;
using System.Collections.Generic;
using System.Text;

namespace Assimalign.OGraph.EntityModel.Tests;

public record class Name
{
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public Affix? Affix { get; set; }
}
