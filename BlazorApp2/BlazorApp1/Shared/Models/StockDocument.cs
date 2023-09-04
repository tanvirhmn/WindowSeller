using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class StockDocument
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
}
