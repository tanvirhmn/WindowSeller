using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class ViewStockSettingsMaterialFolderHierarchy
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Alias { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Color { get; set; } = null!;

    public string Type { get; set; } = null!;

    public float BarLength { get; set; }

    public int? StockSettingsId { get; set; }

    public int? FolderHierarchyId { get; set; }
}
