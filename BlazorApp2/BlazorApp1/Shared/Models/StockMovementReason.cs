using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class StockMovementReason
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int? FromWarehouseId { get; set; }

    public int? ToWarehouseId { get; set; }

    public bool? Active { get; set; }

    public string RivileCenter { get; set; } = null!;

    public string GroupName { get; set; } = null!;

    public int? AccountingEventCanceledReasonId { get; set; }

    public bool? IsGenerateAccountingEvent { get; set; }

    public virtual Warehouse? FromWarehouse { get; set; }

    public virtual ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();

    public virtual Warehouse? ToWarehouse { get; set; }
}
