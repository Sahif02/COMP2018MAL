using System;
using System.Collections.Generic;

namespace COMP2001MAL.Models;

public partial class Bookmark
{
    public int BookmarkId { get; set; }

    public int? UserId { get; set; }

    public int? TrailId { get; set; }

    public DateOnly? BookmarkDate { get; set; }

    public virtual TrailService? Trail { get; set; }

    public virtual User? User { get; set; }
}
