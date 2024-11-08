using System;
using System.Collections.Generic;

namespace HarmonySalon.Reponsitories.Entities;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int? CustomerId { get; set; }

    public int? StylistId { get; set; }

    public int? ServiceId { get; set; }

    public DateTime AppointmentDate { get; set; }

    public string? Status { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Service? Service { get; set; }

    public virtual Stylist? Stylist { get; set; }
}
