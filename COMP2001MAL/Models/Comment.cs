using System;
using System.Collections.Generic;

namespace COMP2001MAL.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public int? PostId { get; set; }

    public int? UserId { get; set; }

    public string? Comment1 { get; set; }

    public DateTime? Timestamp { get; set; }

    public virtual Post? Post { get; set; }

    public virtual User? User { get; set; }
}
