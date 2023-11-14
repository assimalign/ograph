using System;

namespace Assimalign.ErpCore.Entities;

public record class EmployeeDetails
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public DateOnly? DateOfBirth { get; set; }
}
