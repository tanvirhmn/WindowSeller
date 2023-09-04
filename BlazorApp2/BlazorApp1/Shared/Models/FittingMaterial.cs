using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class FittingMaterial
{
    public int MaterialId { get; set; }

    public int FittingId { get; set; }

    public virtual Fitting Fitting { get; set; } = null!;
}
