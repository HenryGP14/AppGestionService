using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Error
{
    public int ErrorId { get; set; }

    public string Nameprocess { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTimeOffset Datecreation { get; set; }

    public string Usercreation { get; set; } = null!;

    public string Ipcreation { get; set; } = null!;
}
