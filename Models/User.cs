using System;
using System.Collections.Generic;

namespace JwtAuthExample.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Contact { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? ProfilePicture { get; set; }

    public DateTime? TimeStamp { get; set; }
}
