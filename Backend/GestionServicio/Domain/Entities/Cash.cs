using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Cash
{
    public int Cashid { get; set; }

    public string Cashdescription { get; set; } = null!;

    public string Active { get; set; } = null!;

    public DateTimeOffset Datecreation { get; set; }

    public DateTimeOffset? Dateupdate { get; set; }

    public DateTimeOffset? Datedelete { get; set; }

    public virtual ICollection<Turn> Turns { get; set; } = new List<Turn>();

    public virtual ICollection<Usercash> Usercashes { get; set; } = new List<Usercash>();
}
