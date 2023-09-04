using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class ProductionTrolley
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int Number { get; set; }

    public virtual ICollection<ProductionTrolleyElement> ProductionTrolleyElements { get; set; } = new List<ProductionTrolleyElement>();
}
