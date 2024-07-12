using System;

namespace Erp;

public class EmployeeInfo
{
    public Name? FirstName { get; set; }
    public Name? LastName { get; set; }
    public Name? MiddleName { get; set; }
    public DateOnly? Birthdate { get; set; }
}