﻿using System;
using System.Collections.Generic;

namespace JwtAuthExample.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Contact { get; set; } = null!;

    public string ProfilePicture { get; set; } = null!;

    public string? Email { get; set; }
}
