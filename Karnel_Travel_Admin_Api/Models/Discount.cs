﻿using System;
using System.Collections.Generic;

namespace Karnel_Travel_Admin_Api.Models;

public partial class Discount
{
    public string? DiscountCode { get; set; }

    public float DiscountPercent { get; set; }
}
