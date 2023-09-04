using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class FittingScrew
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public int X { get; set; }

    public int FittingSide { get; set; }

    public int FittingId { get; set; }

    public int? ScewLength { get; set; }

    public virtual Fitting Fitting { get; set; } = null!;
}
