using System;
using System.Collections.Generic;

namespace AssetManagement.Data;

public partial class Asset
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Make { get; set; }

    public string? SerialNumber { get; set; }

    public Guid StationId { get; set; }

    public Guid CategoryId { get; set; }

    public string Number { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public Guid CreatorId { get; set; }

    public Guid? ServiceCycleId { get; set; }

    public byte[] ImageData { get; set; } = null!;

    public virtual ICollection<AssetAttribute> AssetAttributes { get; } = new List<AssetAttribute>();

    public virtual AssetCategory Category { get; set; } = null!;

    public virtual User Creator { get; set; } = null!;

    public virtual ICollection<Notification> Notifications { get; } = new List<Notification>();

    public virtual ServiceCycle? ServiceCycle { get; set; }

    public virtual ICollection<Service> Services { get; } = new List<Service>();

    public virtual Station Station { get; set; } = null!;
}
