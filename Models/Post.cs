using System;
using System.Collections.Generic;

namespace JwtAuthExample.Models;

public partial class Post
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string PictureUrl { get; set; } = null!;

    public DateTime TimeStamp { get; set; }
}
