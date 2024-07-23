namespace Api.DTO.Absence
{
    public class AbsenceUserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; } = String.Empty;
        public int EmployeeId { get; set; }
        public string Name { get; set; } = String.Empty;
        public string PhotoUrl { get; set; } = String.Empty;
        public int JobId { get; set; }
        public string JobName { get; set; } = String.Empty;
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = String.Empty;
        public int CityId { get; set; }
        public string CityName { get; set; } = String.Empty;
        public int ReportJobId { get; set; }
        public string ReportJobName { get; set; } = String.Empty;
    }
}