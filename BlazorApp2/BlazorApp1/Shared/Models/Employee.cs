using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class Employee
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string WorkPosition { get; set; } = null!;

    public string? Team { get; set; }

    public string? UserName { get; set; }

    public string? SqlUserName { get; set; }

    public virtual ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();

    public virtual ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
}
