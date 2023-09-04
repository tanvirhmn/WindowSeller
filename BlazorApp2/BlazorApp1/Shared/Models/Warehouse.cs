using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class Warehouse
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<StockMovementReason> StockMovementReasonFromWarehouses { get; set; } = new List<StockMovementReason>();

    public virtual ICollection<StockMovementReason> StockMovementReasonToWarehouses { get; set; } = new List<StockMovementReason>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
