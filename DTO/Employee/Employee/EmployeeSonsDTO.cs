namespace Api.DTO.Employee
{
    public class EmployeeSonsDTO
    {
        public int Id { get; set; }
        public int EmployeeGeneralId { get; set; }
        public string? SonName { get; set; }
        public DateTime SonBornDate { get; set; }
        public int Age { get; set; }
    }
}