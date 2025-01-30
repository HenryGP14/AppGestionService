using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Rol
{
    public int Rolid { get; set; }

    public string Rolname { get; set; } = null!;

    public DateTimeOffset Datecreation { get; set; }

    public DateTimeOffset? Dateupdate { get; set; }

    public DateTimeOffset? Datedelete { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
