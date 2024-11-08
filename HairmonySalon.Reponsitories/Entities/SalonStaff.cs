using System;
using System.Collections.Generic;

namespace HarmonySalon.Reponsitories.Entities;

public partial class SalonStaff
{
    public int StaffId { get; set; }

    public string Availability { get; set; } = null!;

    public virtual User Staff { get; set; } = null!;

    public virtual Stylist? Stylist { get; set; }
}
