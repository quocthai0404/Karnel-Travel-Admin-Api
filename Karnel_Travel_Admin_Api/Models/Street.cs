using System;
using System.Collections.Generic;

namespace Karnel_Travel_Admin_Api.Models;

public partial class Street
{
    public int StreetId { get; set; }

    public string StreetName { get; set; } = null!;

    public int WardId { get; set; }

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

    public virtual Ward Ward { get; set; } = null!;
}
