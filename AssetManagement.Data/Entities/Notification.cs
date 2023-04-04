using System;
using System.Collections.Generic;

namespace AssetManagement.Data;

public partial class Notification
{
    public Guid Id { get; set; }

    public Guid AssetId { get; set; }

    public virtual Asset Asset { get; set; } = null!;
}
