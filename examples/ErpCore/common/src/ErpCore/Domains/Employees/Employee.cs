using Assimalign.OGraph;
using ErpCore.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ErpCore.Employees;

public class Employee
{
    public EmployeeId Id { get; set; }
    public EmailAddress Email { get; set; }
    public EmployeeInfo? Info { get; set; }
}