using System;
using System.Collections.Generic;

namespace AssetManagement.Data;

public partial class ServiceCycle
{
    public Guid Id { get; set; }

    public int Period { get; set; }

    public int TypeId { get; set; }

    public virtual ICollection<Asset> Assets { get; } = new List<Asset>();
}
