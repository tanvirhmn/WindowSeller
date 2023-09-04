using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class SalesElement
{
    public int Id { get; set; }

    public string PositionName { get; set; } = null!;

    public int ProductionLot { get; set; }

    public int ProductionSet { get; set; }

    public string Barcode { get; set; } = null!;

    public string Element { get; set; } = null!;

    public string Quantity { get; set; } = null!;

    public double Height { get; set; }

    public double Width { get; set; }

    public bool? HasGrids { get; set; }

    public int LayerInPallet { get; set; }

    public string PalletName { get; set; } = null!;

    public string RotationInPallet { get; set; } = null!;

    public string RowInPallet { get; set; } = null!;

    public int Position { get; set; }

    public double Weight { get; set; }

    public virtual ICollection<SalesElementGlass> SalesElementGlasses { get; set; } = new List<SalesElementGlass>();
}
