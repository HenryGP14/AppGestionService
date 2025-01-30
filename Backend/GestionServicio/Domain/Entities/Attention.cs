using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Attention
{
    public int Attentionid { get; set; }

    public int TurnTurnid { get; set; }

    public int ClientClientid { get; set; }

    public int AttentiontypeAttentiontypeid { get; set; }

    public int AttentionstatusStatusid { get; set; }

    public DateTimeOffset Datecreation { get; set; }

    public DateTimeOffset? Dateupdate { get; set; }

    public DateTimeOffset? Datedelete { get; set; }

    public virtual Client ClientClient { get; set; } = null!;

    public virtual Turn TurnTurn { get; set; } = null!;
}
