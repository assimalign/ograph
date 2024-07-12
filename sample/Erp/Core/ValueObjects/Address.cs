namespace Erp;

public record class Address
{
    public string? StreetOne { get; set; }
    public string? StreetTwo { get; set; }
    public string? StreetThree { get; set; }
    public string? StreetFour { get; set; }
    public string? ZipCode { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
    public State? State { get; set; }
}