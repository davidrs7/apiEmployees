namespace Api.DTO.User
{
    public class UserDTO
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public Boolean Available { get; set; }
        public string UserName { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public int EmployeeId { get; set; }
        public string Name { get; set; } = String.Empty;
        public string PhotoUrl { get; set; } = String.Empty;
        public int JobId { get; set; }
        public string JobName { get; set; } = String.Empty;
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = String.Empty;
    }
}