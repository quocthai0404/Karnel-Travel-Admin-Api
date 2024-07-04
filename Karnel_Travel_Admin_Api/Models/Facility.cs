﻿using System;
using System.Collections.Generic;

namespace Karnel_Travel_Admin_Api.Models;

public partial class Facility
{
    public int FacilityId { get; set; }

    public string FacilityName { get; set; } = null!;

    public bool IsHide { get; set; }

    public virtual ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
}