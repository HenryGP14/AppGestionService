using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Turn
{
    public int Turnid { get; set; }

    public string Description { get; set; } = null!;

    public DateTimeOffset Date { get; set; }

    public int CashCashid { get; set; }

    public int Usergestorid { get; set; }

    public DateTimeOffset Datecreation { get; set; }

    public DateTimeOffset? Dateupdate { get; set; }

    public DateTimeOffset? Datedelete { get; set; }

    public virtual ICollection<Attention> Attentions { get; set; } = new List<Attention>();

    public virtual Cash CashCash { get; set; } = null!;
}
