using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class MaterialTypeEnumDetail
{
    public int Id { get; set; }

    public int Key { get; set; }

    public string Value { get; set; } = null!;

    public int MaterialTypeEnumMasterId { get; set; }

    public virtual MaterialTypeEnumMaster MaterialTypeEnumMaster { get; set; } = null!;
}
