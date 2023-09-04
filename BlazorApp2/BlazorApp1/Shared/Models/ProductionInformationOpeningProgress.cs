using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class ProductionInformationOpeningProgress
{
    public int Id { get; set; }

    public int Yesterday1ShiftProducedOpenings { get; set; }

    public int Yesterday2ShiftProducedOpenings { get; set; }

    public int Produced1ShiftOpenings { get; set; }

    public int Produced2ShiftOpenings { get; set; }

    public int CurrentShiftResults { get; set; }

    public int ExpectedCurrentDifference { get; set; }
}
