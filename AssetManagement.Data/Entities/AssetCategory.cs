using System;
using System.Collections.Generic;

namespace AssetManagement.Data;

public partial class AssetCategory
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Asset> Assets { get; } = new List<Asset>();
}
