using System;
using System.Collections.Generic;

namespace HarmonySalon.Reponsitories.Entities;

public partial class Customer
{
    public int CustomerId { get; set; }

    public int? LoyaltyPoints { get; set; }

    public string? Appointments { get; set; }

    public virtual ICollection<Appointment> AppointmentsNavigation { get; set; } = new List<Appointment>();

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual User CustomerNavigation { get; set; } = null!;
}
