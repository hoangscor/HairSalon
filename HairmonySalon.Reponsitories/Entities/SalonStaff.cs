using System;
using System.Collections.Generic;

namespace Harmony.Repositories.Entities;

public partial class SalonStaff
{
    public int StaffId { get; set; }

    public string Availability { get; set; } = null!;

    public virtual User Staff { get; set; } = null!;
}
