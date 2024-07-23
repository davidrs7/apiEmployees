namespace Api.DTO.User
{
    public class SessionDTO
    {
        public string Token { get; set; } = String.Empty;
        public int UserId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Reload { get; set; }
        public string IpAddress { get; set; } = String.Empty;
    }
}