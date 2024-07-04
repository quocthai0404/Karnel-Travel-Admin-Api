using System;
using System.Collections.Generic;

namespace Karnel_Travel_Admin_Api.Models;

public partial class AdminAccount
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}
