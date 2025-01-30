using System.Text.Json.Serialization;

namespace Application.Dtos.Request
{
    public class CashRequest
    {
        public string? Description { get; set; }
        public string? Status { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
    }
}
