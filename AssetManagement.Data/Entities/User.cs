using System;
using System.Collections.Generic;

namespace AssetManagement.Data;

public partial class User
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Mobile { get; set; }

    public Guid? OrganizationId { get; set; }

    public DateTime CreationDate { get; set; }

    public int? RightsId { get; set; }

    public string? PreferredEmail { get; set; }

    public string? PasswordHash { get; set; }

    public bool IsActive { get; set; }

    public bool IsEmailConfirmed { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public bool TwoFactorAuthEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public Guid? CreatorId { get; set; }

    public string LoginId { get; set; } = null!;

    public bool IsMobileConfirmed { get; set; }

    public string? SecurityStamp { get; set; }

    public DateTime? ActivationDate { get; set; }

    public Guid? UserGroupId { get; set; }

    public DateTime? LockoutExpiryDate { get; set; }

    public string? AuthRecoveryCodes { get; set; }

    public string? AuthenticatorKey { get; set; }

    public virtual ICollection<Asset> Assets { get; } = new List<Asset>();
}
