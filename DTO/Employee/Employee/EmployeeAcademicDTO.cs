namespace Api.DTO.Employee
{
    public class EmployeeAcademicDTO
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int? EducationalLevelId { get; set; }
        public string? EducationalLevelName { get; set; } = String.Empty;
        public string? Career { get; set; } = String.Empty;
    }
}