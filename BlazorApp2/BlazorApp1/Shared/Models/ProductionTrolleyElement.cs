using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class ProductionTrolleyElement
{
    public int Id { get; set; }

    public int TrolleyId { get; set; }

    public int ElementId { get; set; }

    public virtual ProductionTrolley Trolley { get; set; } = null!;
}
