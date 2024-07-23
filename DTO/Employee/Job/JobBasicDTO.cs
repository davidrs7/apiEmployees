namespace Api.DTO.Job
{
    public class JobBasicDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public int Employees { get; set; }
        public int EmployeesInProcess { get; set; } = 0;
    }
}