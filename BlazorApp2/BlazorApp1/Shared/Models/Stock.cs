using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class Stock
{
    public int Id { get; set; }

    public int MaterialId { get; set; }

    public int WarehouseId { get; set; }

    public double Length { get; set; }

    public double Height { get; set; }

    public double Quantity { get; set; }

    public DateTime LastDocumentDate { get; set; }

    public virtual Material Material { get; set; } = null!;

    public virtual ICollection<StockMovement> StockMovementFromStocks { get; set; } = new List<StockMovement>();

    public virtual ICollection<StockMovement> StockMovementToStocks { get; set; } = new List<StockMovement>();

    public virtual Warehouse Warehouse { get; set; } = null!;
}
