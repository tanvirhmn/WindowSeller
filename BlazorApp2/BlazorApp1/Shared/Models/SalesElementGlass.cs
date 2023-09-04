using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class SalesElementGlass
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public int SalesElementId { get; set; }

    public string Barcode { get; set; } = null!;

    public string RackName { get; set; } = null!;

    public int Number { get; set; }

    public bool? Ordered { get; set; }

    public string Priority { get; set; } = null!;

    public bool? Received { get; set; }

    public int Version { get; set; }

    public int ProductionLot { get; set; }

    public int ProductionSet { get; set; }

    public bool? IsAwning { get; set; }

    public double Height { get; set; }

    public double Width { get; set; }

    public bool? Glazed { get; set; }

    public bool? InBuffer { get; set; }

    public virtual SalesElement SalesElement { get; set; } = null!;
}
