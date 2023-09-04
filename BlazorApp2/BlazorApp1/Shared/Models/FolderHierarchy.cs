using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class FolderHierarchy
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? ParentId { get; set; }

    public string Icon { get; set; } = null!;

    public virtual ICollection<FolderHierarchyMaterialColumnMap> FolderHierarchyMaterialColumnMaps { get; set; } = new List<FolderHierarchyMaterialColumnMap>();

    public virtual ICollection<FolderHierarchy> InverseParent { get; set; } = new List<FolderHierarchy>();

    public virtual FolderHierarchy? Parent { get; set; }

    public virtual ICollection<StockSetting> StockSettings { get; set; } = new List<StockSetting>();
}
