using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Payment
{
    public int Paymentid { get; set; }

    public DateTimeOffset Paymentdate { get; set; }

    public int ClientClientid { get; set; }

    public DateTimeOffset Datecreation { get; set; }

    public DateTimeOffset? Dateupdate { get; set; }

    public DateTimeOffset? Datedelete { get; set; }

    public virtual Client ClientClient { get; set; } = null!;
}
