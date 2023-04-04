using System;
using System.Collections.Generic;

namespace AssetManagement.Data;

public partial class Service
{
    public Guid Id { get; set; }

    public DateOnly ServiceDate { get; set; }

    public string? Description { get; set; }

    public Guid AssetId { get; set; }

    public virtual Asset Asset { get; set; } = null!;
}
