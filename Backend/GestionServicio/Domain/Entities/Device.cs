using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Device
{
    public int Deviceid { get; set; }

    public string Devicename { get; set; } = null!;

    public int ServiceServiceid { get; set; }

    public DateTimeOffset Datecreation { get; set; }

    public DateTimeOffset? Dateupdate { get; set; }

    public DateTimeOffset? Datedelete { get; set; }

    public virtual Service ServiceService { get; set; } = null!;
}
