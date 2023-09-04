using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class Fitting
{
    public int Id { get; set; }

    public int? Backset { get; set; }

    public bool? EndCuttable { get; set; }

    public int? EndCutMaxLength { get; set; }

    public int? HandlePosition { get; set; }

    public int? Length { get; set; }

    public int? SecondaryLength { get; set; }

    public int? StartCutMaxLength { get; set; }

    public bool? StartCuttable { get; set; }

    public int FittingType { get; set; }

    public int FittingLocation { get; set; }

    public int? HandlePositionType { get; set; }

    public string Alias { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string PrefFittingsId { get; set; } = null!;

    public string PrefMaterialBaseCode { get; set; } = null!;

    public virtual ICollection<FittingHardwareSetDescription> FittingHardwareSetDescriptions { get; set; } = new List<FittingHardwareSetDescription>();

    public virtual ICollection<FittingMushroom> FittingMushrooms { get; set; } = new List<FittingMushroom>();

    public virtual ICollection<FittingScrew> FittingScrews { get; set; } = new List<FittingScrew>();
}
