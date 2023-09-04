using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class PurchaseInvoiceImport
{
    public int Id { get; set; }

    public int PurcahseNumber { get; set; }

    public int Type { get; set; }

    public string? MaterialCode { get; set; }

    public int Quantity { get; set; }

    public double Price { get; set; }

    public double TotalPrice { get; set; }

    public string? Additional { get; set; }
}
