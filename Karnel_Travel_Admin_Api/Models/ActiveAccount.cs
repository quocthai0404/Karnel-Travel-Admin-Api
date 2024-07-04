using System;
using System.Collections.Generic;

namespace Karnel_Travel_Admin_Api.Models;

public partial class ActiveAccount
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string SecurityCode { get; set; } = null!;

    public bool IsActive { get; set; }
}
