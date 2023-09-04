using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class ExternalMovementsArchive
{
    public int Id { get; set; }

    public string MaterialCode { get; set; } = null!;

    public double Length { get; set; }

    public double Height { get; set; }

    public double Quantity { get; set; }

    public string ReasonName { get; set; } = null!;

    public string Comment { get; set; } = null!;

    public int DocumentNumber { get; set; }

    public string DocumentName { get; set; } = null!;

    public DateTime DocumentDate { get; set; }

    public string UserName { get; set; } = null!;
}
