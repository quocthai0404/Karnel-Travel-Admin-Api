using System;
using System.Collections.Generic;

namespace Karnel_Travel_Admin_Api.Modelsss;

public partial class Restaurant
{
    public int RestaurantId { get; set; }

    public string RestaurantName { get; set; } = null!;

    public string RestaurantDescription { get; set; } = null!;

    public string RestaurantPriceRange { get; set; } = null!;

    public string? RestaurantLocation { get; set; }

    public int? LocationId { get; set; }

    public bool IsHide { get; set; }

    public virtual Location? Location { get; set; }

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
