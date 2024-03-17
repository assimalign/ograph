using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assimalign.OGraph.Gdm.Tests;

public record class Address
{
    public string? StreetOne { get; set; }
    public string? StreetTwo { get; set; }
    public string? StreetThree { get; set; }
    public string? ZipCode { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
}
