namespace Application.Dtos.Response
{
    public class UserReponse
    {
        public int Userid { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Rol { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTimeOffset? Creationdate { get; set; }
        public bool Approval { get; set; }
        public DateTimeOffset? ApprovalDate { get; set; }
        public string? UserApproval { get; set; }
    }
}
