using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class ViewDmcTabular
{
    public int RowNo { get; set; }

    public string? MaterialId { get; set; }

    public string? Code { get; set; }

    public string? Alias { get; set; }

    public string? Description { get; set; }

    public string? Color { get; set; }

    public string? Type { get; set; }

    public string? BarLength { get; set; }

    public string? CollectionType { get; set; }

    public string? Reproducible { get; set; }

    public string? FolderHierarchyId { get; set; }
}
