using System;
using System.Collections.Generic;
using System.Text;

namespace ErpCore;

public record class Address
{
    public string? StreetOne { get; set; }
    public string? StreetTwo { get; set; }
    public string? StreetThree { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
}
