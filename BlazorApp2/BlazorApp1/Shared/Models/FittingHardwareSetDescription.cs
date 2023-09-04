using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class FittingHardwareSetDescription
{
    public int Id { get; set; }

    public int FittingId { get; set; }

    public int? MinWidth { get; set; }

    public int? MaxWidth { get; set; }

    public int? MinHeight { get; set; }

    public int? MaxHeight { get; set; }

    public int Position { get; set; }

    public int ReferencePoint { get; set; }

    public int ChainPosition { get; set; }

    public bool? Inverted { get; set; }

    public int FittingHardwareSetId { get; set; }

    public int? Alternative { get; set; }

    public bool? DefaultHandle { get; set; }

    public int MinSashWeight { get; set; }

    public int MaxSashWeight { get; set; }

    public string? FormulaX { get; set; }

    public virtual Fitting Fitting { get; set; } = null!;

    public virtual FittingHardwareSet FittingHardwareSet { get; set; } = null!;
}
