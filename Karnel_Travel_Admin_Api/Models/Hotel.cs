﻿using System;
using System.Collections.Generic;

namespace Karnel_Travel_Admin_Api.Models;

public partial class Hotel
{
    public int HotelId { get; set; }

    public string HotelName { get; set; } = null!;

    public string? HotelDescription { get; set; }

    public string? HotelPriceRange { get; set; }

    public string? HotelLocation { get; set; }

    public int? LocationId { get; set; }

    public bool IsHide { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Location? Location { get; set; }

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();

    public virtual ICollection<Facility> Facilities { get; set; } = new List<Facility>();
}
