using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class FolderHierarchyMaterialColumnMap
{
    public int Id { get; set; }

    public bool? IsRequired { get; set; }

    public bool? IsVisible { get; set; }

    public int FolderHierarchyId { get; set; }

    public int MaterialColumnId { get; set; }

    public virtual FolderHierarchy FolderHierarchy { get; set; } = null!;

    public virtual MaterialColumn MaterialColumn { get; set; } = null!;
}
