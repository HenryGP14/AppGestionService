﻿using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Methodpayment
{
    public int Methodpaymentid { get; set; }

    public string Description { get; set; } = null!;

    public DateTimeOffset Datecreation { get; set; }

    public DateTimeOffset? Dateupdate { get; set; }

    public DateTimeOffset? Datedelete { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}
