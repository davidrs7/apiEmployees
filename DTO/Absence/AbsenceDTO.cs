namespace Api.DTO.Absence
{
    public class AbsenceDTO
    {
        public int Id { get; set; }
        public int AbsenceTypeId { get; set; }
        public string AbsenceTypeName { get; set; } = String.Empty;
        public int UserId { get; set; }
        public string UserName { get; set; } = String.Empty;
        public string UserEmployeeName { get; set; } = String.Empty;
        public string UserPhotoUrl { get; set; } = String.Empty;
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = String.Empty;
        public string EmployeePhotoUrl { get; set; } = String.Empty;
        public int JobId { get; set; }
        public string JobName { get; set; } = String.Empty;
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = String.Empty;
        public int ReportJobId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }
        public int BusinessDays { get; set; }
        public int Active { get; set; }
        public int Status { get; set; }
        public string Description { get; set; } = String.Empty;
        public int ApprovalQuantity { get; set; }
    }
}