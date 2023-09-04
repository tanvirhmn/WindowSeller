using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class MaterialColumnValue
{
    public int Id { get; set; }

    public int RowNo { get; set; }

    public string Value { get; set; } = null!;

    public int MaterialColumnId { get; set; }

    public virtual MaterialColumn MaterialColumn { get; set; } = null!;
}
