using System;
using System.Collections.Generic;

namespace Harmony.Repositories.Entities;

public partial class Manager
{
    public int ManagerId { get; set; }

    public string? Permissions { get; set; }

    public virtual User ManagerNavigation { get; set; } = null!;
}
