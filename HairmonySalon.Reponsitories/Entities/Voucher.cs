using System;
using System.Collections.Generic;

namespace HarmonySalon.Reponsitories.Entities;

public partial class Voucher
{
    public int VoucherId { get; set; }

    public string? Description { get; set; }

    public decimal? DiscountRate { get; set; }

    public string? ValidityPeriod { get; set; }
}
