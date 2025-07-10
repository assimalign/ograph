using System;
using System.Collections.Generic;
using System.Text;

namespace ErpCore.Employees;

public class EmployeeJob
{
    public JobId Id { get; set; }
    public AuditField? Created { get; set; }
    public AuditField? Updated { get; set; }
}
