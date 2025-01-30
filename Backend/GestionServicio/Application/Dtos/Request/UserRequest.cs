using System.Text.Json.Serialization;

namespace Application.Dtos.Request
{
    public class UserRequest
    {
        [JsonIgnore]
        public int UserAuthId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int Rolid { get; set; }
        public int Statusid { get; set; }
    }
}
