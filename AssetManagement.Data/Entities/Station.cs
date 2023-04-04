using System;
using System.Collections.Generic;

namespace AssetManagement.Data;

public partial class Station
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Asset> Assets { get; } = new List<Asset>();
}
