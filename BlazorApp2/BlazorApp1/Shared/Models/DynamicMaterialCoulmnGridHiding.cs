using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class DynamicMaterialCoulmnGridHiding
{
    public int Id { get; set; }

    public string? UserId { get; set; }

    public int MaterialColumnId { get; set; }

    public virtual MaterialColumn MaterialColumn { get; set; } = null!;
}
