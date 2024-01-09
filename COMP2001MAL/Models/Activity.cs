using System;
using System.Collections.Generic;

namespace COMP2001MAL.Models;

public partial class Activity
{
    public int ActivityId { get; set; }

    public int? UserId { get; set; }

    public int? TrailId { get; set; }

    public string? ActivityType { get; set; }

    public int? Duration { get; set; }

    public int? Distance { get; set; }

    public DateOnly? ActivityDate { get; set; }

    public string? Status { get; set; }

    public virtual TrailService? Trail { get; set; }

    public virtual User? User { get; set; }
}
