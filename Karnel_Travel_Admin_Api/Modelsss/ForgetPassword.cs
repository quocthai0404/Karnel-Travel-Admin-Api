﻿using System;
using System.Collections.Generic;

namespace Karnel_Travel_Admin_Api.Modelsss;

public partial class ForgetPassword
{
    public string Email { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTime Expire { get; set; }

    public int Id { get; set; }
}