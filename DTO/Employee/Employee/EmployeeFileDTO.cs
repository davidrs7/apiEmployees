namespace Api.DTO.Employee
{
    public class EmployeeFileDTO
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Department { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
        public string City { get; set; } = String.Empty;
        public string Level1 { get; set; } = String.Empty;
        public string Level2 { get; set; } = String.Empty;
        public string Level3 { get; set; } = String.Empty;
        public string Url { get; set; } = String.Empty;
        public string FileName { get; set; } = String.Empty;
    }
}