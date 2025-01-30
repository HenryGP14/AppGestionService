using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class User
{
    public int Userid { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RolRolid { get; set; }

    public DateTimeOffset? Creationdate { get; set; }

    public int? Usercreate { get; set; }

    public int? Userapproval { get; set; }

    public DateTimeOffset? Dateapproval { get; set; }

    public int UserstatusStatusid { get; set; }

    public DateTimeOffset Datecreation { get; set; }

    public DateTimeOffset? Dateupdate { get; set; }

    public DateTimeOffset? Datedelete { get; set; }

    public virtual Rol RolRol { get; set; } = null!;

    public virtual ICollection<Usercash> Usercashes { get; set; } = new List<Usercash>();

    public virtual Userstatus UserstatusStatus { get; set; } = null!;
}
