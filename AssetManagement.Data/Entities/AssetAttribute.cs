using System;
using System.Collections.Generic;

namespace AssetManagement.Data;

public partial class AssetAttribute
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly? ExpiryDate { get; set; }

    public Guid AssetId { get; set; }

    public virtual Asset Asset { get; set; } = null!;
}
