using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class StockAccountingMovement
{
    public int Id { get; set; }

    public int StockMovementId { get; set; }

    public int? ChangedByStockMovementId { get; set; }

    public int Status { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public virtual StockMovement? ChangedByStockMovement { get; set; }

    public virtual ICollection<StockAccountingAction> StockAccountingActions { get; set; } = new List<StockAccountingAction>();

    public virtual StockMovement StockMovement { get; set; } = null!;
}
