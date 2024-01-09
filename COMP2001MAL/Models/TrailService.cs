using System;
using System.Collections.Generic;

namespace COMP2001MAL.Models;

public partial class TrailService
{
    public int TrailId { get; set; }

    public string? TrailName { get; set; }

    public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();

    public virtual ICollection<Bookmark> Bookmarks { get; set; } = new List<Bookmark>();
}
