using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class StockSetting
{
    public int Id { get; set; }

    public int MaterialId { get; set; }

    public string CollectionType { get; set; } = null!;

    public bool? Reproducible { get; set; }

    public int? FolderHierarchyId { get; set; }

    public virtual FolderHierarchy? FolderHierarchy { get; set; }

    public virtual Material Material { get; set; } = null!;
}
