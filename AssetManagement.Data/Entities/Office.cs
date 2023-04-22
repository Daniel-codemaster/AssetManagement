using System;
using System.Collections.Generic;

namespace AssetManagement.Data;

public partial class Office
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid? StationId { get; set; }

    public virtual ICollection<Asset> Assets { get; } = new List<Asset>();

    public virtual Station? Station { get; set; }
}
