using System;
using System.Collections.Generic;

namespace Karnel_Travel_Admin_Api.Modelsss;

public partial class Beach
{
    public int BeachId { get; set; }

    public string BeachName { get; set; } = null!;

    public string? BeachLocation { get; set; }

    public int? LocationId { get; set; }

    public bool IsHide { get; set; }

    public virtual Location? Location { get; set; }

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();
}
