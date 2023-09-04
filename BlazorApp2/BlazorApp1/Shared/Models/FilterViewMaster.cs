using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class FilterViewMaster
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string AzureUserId { get; set; } = null!;

    public virtual ICollection<FilterViewDetail> FilterViewDetails { get; set; } = new List<FilterViewDetail>();
}
