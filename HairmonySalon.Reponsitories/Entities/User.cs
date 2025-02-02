﻿using System;
using System.Collections.Generic;

namespace Harmony.Repositories.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Phone { get; set; }

    public string UserType { get; set; } = null!;

    public virtual ICollection<Appointment> AppointmentCustomers { get; set; } = new List<Appointment>();

    public virtual ICollection<Appointment> AppointmentStylists { get; set; } = new List<Appointment>();

    public virtual Customer? Customer { get; set; }

    public virtual Manager? Manager { get; set; }

    public virtual SalonStaff? SalonStaff { get; set; }
}
