namespace Assimalign.OGraph.Gdm;

public record class EmployeeAddress : EmployeeBase<EmployeeAddress>
{
    public Guid? AddressId { get; set; }
    public string? StreetOne { get; set; }
    public string? StreetTwo { get; set; }
    public string? StreetThree { get; set; }
    public string? ZipCode { get; set; }
    public string? City { get; set; }
}
