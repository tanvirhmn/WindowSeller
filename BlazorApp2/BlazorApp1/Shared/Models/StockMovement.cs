using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class StockMovement
{
    public int Id { get; set; }

    public double Quantity { get; set; }

    public int EmployeeId { get; set; }

    public int ReasonId { get; set; }

    public string Comment { get; set; } = null!;

    public int DocumentNumber { get; set; }

    public DateTime InsertDate { get; set; }

    public DateTime DocumentDate { get; set; }

    public int DocumentId { get; set; }

    public int? FromStockId { get; set; }

    public double FromTotalQuantity { get; set; }

    public int? ToStockId { get; set; }

    public double ToTotalQuantity { get; set; }

    public virtual StockDocument Document { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual Stock? FromStock { get; set; }

    public virtual StockMovementReason Reason { get; set; } = null!;

    public virtual ICollection<StockAccountingMovement> StockAccountingMovementChangedByStockMovements { get; set; } = new List<StockAccountingMovement>();

    public virtual ICollection<StockAccountingMovement> StockAccountingMovementStockMovements { get; set; } = new List<StockAccountingMovement>();

    public virtual Stock? ToStock { get; set; }
}
