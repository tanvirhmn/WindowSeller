using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class MaterialColumn
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Block { get; set; } = null!;

    public string Type { get; set; } = null!;

    public bool? IsLocked { get; set; }

    public int? MaterialTypeEnumMasterId { get; set; }

    public virtual ICollection<DynamicMaterialCoulmnGridHiding> DynamicMaterialCoulmnGridHidings { get; set; } = new List<DynamicMaterialCoulmnGridHiding>();

    public virtual ICollection<FolderHierarchyMaterialColumnMap> FolderHierarchyMaterialColumnMaps { get; set; } = new List<FolderHierarchyMaterialColumnMap>();

    public virtual ICollection<MaterialColumnValue> MaterialColumnValues { get; set; } = new List<MaterialColumnValue>();

    public virtual MaterialTypeEnumMaster? MaterialTypeEnumMaster { get; set; }
}
