using System;
using System.Collections.Generic;

namespace HarmonySalon.Reponsitories.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Phone { get; set; }

    public string UserType { get; set; } = null!;

    public virtual Customer? Customer { get; set; }

    public virtual SalonStaff? SalonStaff { get; set; }
}
