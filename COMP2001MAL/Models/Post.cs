using System;
using System.Collections.Generic;

namespace COMP2001MAL.Models;

public partial class Post
{
    public int PostId { get; set; }

    public int? UserId { get; set; }

    public string? Content { get; set; }

    public DateTime? Timestamp { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User? User { get; set; }
}
