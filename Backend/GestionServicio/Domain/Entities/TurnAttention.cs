using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class TurnAttention
    {
        public string? Description { get; set; }
        public DateTimeOffset Date { get; set; } = DateTime.Now;
        public int CashId { get; set; }
        public int ClientId { get; set; }
        public int AttentionTypeId { get; set; }
        public int AttentionStateId { get; set; }
        [JsonIgnore]
        public int UsarAuthId { get; set; }
    }
}
