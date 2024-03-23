using System;

namespace Erp;

public class EmployeeInfo
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public DateOnly? Birthdate { get; set; }
}