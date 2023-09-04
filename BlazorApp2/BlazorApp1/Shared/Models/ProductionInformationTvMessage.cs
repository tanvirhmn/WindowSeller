using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class ProductionInformationTvMessage
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public string InfoLine { get; set; } = null!;

    public TimeSpan ShowFromTime { get; set; }

    public TimeSpan ShowToTime { get; set; }

    public string CreatedBy { get; set; } = null!;

    public bool? Disabled { get; set; }

    public string DisabledBy { get; set; } = null!;
}
