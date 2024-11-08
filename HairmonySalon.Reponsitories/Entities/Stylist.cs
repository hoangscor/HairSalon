using System;
using System.Collections.Generic;

namespace HarmonySalon.Reponsitories.Entities;

public partial class Stylist
{
    public int StylistId { get; set; }

    public string? SkillLevel { get; set; }

    public string Availability { get; set; } = null!;

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual SalonStaff StylistNavigation { get; set; } = null!;
}
