using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class StockAccountingAction
{
    public int Id { get; set; }

    public int StockAccountingMovementId { get; set; }

    public string Request { get; set; } = null!;

    public string Response { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string Method { get; set; } = null!;

    public bool? IsSuccess { get; set; }

    public virtual StockAccountingMovement StockAccountingMovement { get; set; } = null!;
}
