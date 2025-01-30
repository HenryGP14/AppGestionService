using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Usercash
{
    public int UserUserid { get; set; }

    public int CashCashid { get; set; }

    public DateTimeOffset Datecreation { get; set; }

    public DateTimeOffset? Dateupdate { get; set; }

    public DateTimeOffset? Datedelete { get; set; }

    public virtual Cash CashCash { get; set; } = null!;

    public virtual User UserUser { get; set; } = null!;
}
