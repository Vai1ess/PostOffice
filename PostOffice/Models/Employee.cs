using System;
using System.Collections.Generic;

namespace PostOffice.Models;

public partial class Employee
{
    public int IdEmployee { get; set; }

    public int IdBranch { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Position { get; set; }

    public decimal? Salary { get; set; }

    public DateOnly? HireDate { get; set; }

    public virtual Branch IdBranchNavigation { get; set; } = null!;
}
