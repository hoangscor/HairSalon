using System;
using System.Collections.Generic;

namespace Harmony.Repositories.Entities;

public partial class Cart
{
    public int CartId { get; set; }

    public int? CustomerId { get; set; }

    public decimal TotalAmount { get; set; }

    public string? Items { get; set; }

    public virtual Customer? Customer { get; set; }
}
