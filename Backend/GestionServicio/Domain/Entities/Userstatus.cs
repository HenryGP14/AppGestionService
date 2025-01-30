using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Userstatus
{
    public int Statusid { get; set; }

    public string Description { get; set; } = null!;

    public DateTimeOffset Datecreation { get; set; }

    public DateTimeOffset? Dateupdate { get; set; }

    public DateTimeOffset? Datedelete { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
