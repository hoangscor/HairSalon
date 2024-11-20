using System;
using System.Collections.Generic;

namespace Harmony.Repositories.Entities;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int? CustomerId { get; set; }

    public int? StylistId { get; set; }

    public int? ServiceId { get; set; }

    public string? AppointmentDate { get; set; }

    public string? Status { get; set; }

    public virtual User? Customer { get; set; }

    public virtual Service? Service { get; set; }

    public virtual User? Stylist { get; set; }
}
