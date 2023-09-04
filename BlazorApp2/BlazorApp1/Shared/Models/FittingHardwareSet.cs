using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class FittingHardwareSet
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string ManufacturerCode { get; set; } = null!;

    public int Movement { get; set; }

    public string UsedFor { get; set; } = null!;

    public bool Tilt { get; set; }

    public bool Turn { get; set; }

    public bool Sliding { get; set; }

    public string Description { get; set; } = null!;

    public int? MinHeight { get; set; }

    public int? MaxHeight { get; set; }

    public int? MinWidth { get; set; }

    public int? MaxWidth { get; set; }

    public string PrefCode { get; set; } = null!;

    public string? PrefSetId { get; set; }

    public int HandleSide { get; set; }

    public int HingeSide { get; set; }

    public virtual ICollection<FittingHardwareSetDescription> FittingHardwareSetDescriptions { get; set; } = new List<FittingHardwareSetDescription>();
}
