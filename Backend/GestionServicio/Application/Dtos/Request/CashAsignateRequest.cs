using System.Text.Json.Serialization;

namespace Application.Dtos.Request
{
    public class CashAsignateRequest
    {
        public int UserId { get; set; }
        public int CashId { get; set; }
        [JsonIgnore]
        public int UserIdAuth { get; set; }
    }
}
