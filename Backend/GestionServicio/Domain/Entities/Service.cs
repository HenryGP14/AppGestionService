﻿using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Service
{
    public int Serviceid { get; set; }

    public string Servicename { get; set; } = null!;

    public string? Servicedescription { get; set; }

    public string Price { get; set; } = null!;

    public DateTimeOffset Datecreation { get; set; }

    public DateTimeOffset? Dateupdate { get; set; }

    public DateTimeOffset? Datedelete { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
}
