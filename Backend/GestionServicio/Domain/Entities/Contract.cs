using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Contract
{
    public int Contractid { get; set; }

    public DateTimeOffset Startdate { get; set; }

    public DateTimeOffset Enddate { get; set; }

    public int ServiceServiceid { get; set; }

    public int StatuscontractStatusid { get; set; }

    public int ClientClientid { get; set; }

    public int MethodpaymentMethodpaymentid { get; set; }

    public DateTimeOffset Datecreation { get; set; }

    public DateTimeOffset? Dateupdate { get; set; }

    public DateTimeOffset? Datedelete { get; set; }

    public virtual Client ClientClient { get; set; } = null!;

    public virtual Methodpayment MethodpaymentMethodpayment { get; set; } = null!;

    public virtual Service ServiceService { get; set; } = null!;

    public virtual Statuscontract StatuscontractStatus { get; set; } = null!;
}
