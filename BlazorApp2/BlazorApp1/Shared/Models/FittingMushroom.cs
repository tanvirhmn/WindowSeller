using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class FittingMushroom
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public int X { get; set; }

    public int MushroomSide { get; set; }

    public int StrikerPlateType { get; set; }

    public int FittingId { get; set; }

    public virtual Fitting Fitting { get; set; } = null!;
}
