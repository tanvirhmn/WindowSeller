using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class Material
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Alias { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Color { get; set; } = null!;

    public string Type { get; set; } = null!;

    public float BarLength { get; set; }

    public virtual StockSetting? StockSetting { get; set; }

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
