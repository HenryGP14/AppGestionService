namespace Domain.Entities
{
    public class TurnGestion
    {
        public int Turnid { get; set; }
        public string? Description { get; set; }
        public string? UserGestion { get; set; }
        public DateTimeOffset Date { get; set; }
        public string? Cash { get; set; }
        public string? CLient { get; set; }
        public string? Identification { get; set; }
        public string? StatusAttention { get; set; }
        public string? TypeAttention { get; set; }
    }
}
