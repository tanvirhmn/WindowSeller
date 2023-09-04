using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class MaterialsSetting
{
    public int Id { get; set; }

    public int MaterialId { get; set; }

    public string CollectionType { get; set; } = null!;

    public bool Reproducible { get; set; }
}
