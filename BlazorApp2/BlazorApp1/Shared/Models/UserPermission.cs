using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class UserPermission
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public int PermissionId { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Permission Permission { get; set; } = null!;
}
