using System;
using System.Collections.Generic;
using System.Text;

namespace ErpCore.Employees;

public class EmployeeTask
{
    public TaskId Id { get; set; }

    public AuditField? Created { get; set; }
    public AuditField? Updated { get; set; }
}
