﻿using System;
using System.Collections.Generic;

namespace Karnel_Travel_Admin_Api.Models;

public partial class Tour
{
    public int TourId { get; set; }

    public string TourName { get; set; } = null!;

    public string TourDescription { get; set; } = null!;

    public int Departure { get; set; }

    public int Arrival { get; set; }

    public float TourPrice { get; set; }

    public bool IsHide { get; set; }

    public virtual Province ArrivalNavigation { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Province DepartureNavigation { get; set; } = null!;

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();
}
