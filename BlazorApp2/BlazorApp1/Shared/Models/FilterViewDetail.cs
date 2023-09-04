using System;
using System.Collections.Generic;

namespace BlazorApp1.Server.Models;

public partial class FilterViewDetail
{
    public int Id { get; set; }

    public string Property { get; set; } = null!;

    public int? ParentId { get; set; }

    public string FilterValue { get; set; } = null!;

    public string FilterOperator { get; set; } = null!;

    public string LogicalFilterOperator { get; set; } = null!;

    public int FilterViewMasterId { get; set; }

    public virtual FilterViewMaster FilterViewMaster { get; set; } = null!;

    public virtual ICollection<FilterViewDetail> InverseParent { get; set; } = new List<FilterViewDetail>();

    public virtual FilterViewDetail? Parent { get; set; }
}
