using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class MaterialTypeEnumMaster
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual MaterialColumn? MaterialColumn { get; set; }

    public virtual ICollection<MaterialTypeEnumDetail> MaterialTypeEnumDetails { get; set; } = new List<MaterialTypeEnumDetail>();
}
